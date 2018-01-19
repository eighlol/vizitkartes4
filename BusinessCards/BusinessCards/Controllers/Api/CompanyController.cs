using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessCards.Entities;
using BusinessCards.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace Vizitkartes.API.Controllers
{
    
    [Route("api/company")]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ICompanyRepository companyRepository, IBusinessCardRepository businessCardRepository, ILogger<CompanyController> logger, UserManager<ApplicationUser> userManager)
        {
            _companyRepository = companyRepository;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet()]
        public IActionResult GetCompanies()
        {
            var companies = _companyRepository.GetCompanies();
            return Ok(Mapper.Map<IEnumerable<CompanyDto>>(companies));
        }

        [HttpGet("{id}")]
        public IActionResult GetCompany(int id)
        {
            var company = _companyRepository.GetCompany(id);
            if (company == null)
            {
                _logger.LogInformation($"Company with id {id} wasn't found.");
                return NotFound();
            }

            return Ok(Mapper.Map<CompanyDto>(company));
        }

        [HttpGet("{id}/businessCards")]
        public IActionResult GetCompanyBusinessCards(int id)
        {
            var company = _companyRepository.GetCompany(id);
            if (company == null)
            {
                _logger.LogInformation($"Company with id {id} wasn't found.");
                return NotFound();
            }
            var businessCards = _companyRepository.GetCompanyEmployees(company);
            return Ok(Mapper.Map<IEnumerable<BusinessCardDto>>(businessCards));
        }

        [Authorize]
        [HttpPost("{id}/employees")]
        public async Task<IActionResult> SendRequestToBeCompanyEmployee(int id)
        {
            var company = _companyRepository.GetCompany(id);
            if (company == null)
            {
                _logger.LogInformation($"Company with id {id} wasn't found.");
                return NotFound("Company not found!");
            }

            var user = await _userManager.GetUserAsync(User);
            var userActualCompany = _companyRepository.GetUserCompany(user);

            if (userActualCompany == company)
            {
                return StatusCode(409, "User already exists!");
            }

            _companyRepository.MakeCompanyEmployee(user, id);
            if (!_companyRepository.Save())
            {
                _logger.LogError($"Failed to add employee {user.Id} to company {company.Id}.");
                return StatusCode(500, "A problem happened while handling your request.");
            }
            return Ok(Mapper.Map<BusinessCardDto>(user.BusinessCard));
        }

    }
}