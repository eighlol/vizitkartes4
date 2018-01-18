using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vizitkartes.API.Models;
using Vizitkartes.API.Services;

namespace Vizitkartes.API.Controllers
{
    [Route("api/BusinessCard")]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ICompanyRepository companyRepository, ILogger<CompanyController> logger)
        {
            _companyRepository = companyRepository;
            _logger = logger;
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
    }
}