using System.Collections.Generic;
using AutoMapper;
using BusinessCards.Entities;
using BusinessCards.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace BusinessCards.Controllers.Api
{
    [Authorize(Roles = "Administrator")]
    [ValidateAntiForgeryToken]
    [Route("api/administrator")]
    public class AdministratorController : Controller
    {
        private readonly IManagerRepository _managerRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IBusinessCardRepository _businessCardRepository;
        private readonly ILogger<AdministratorController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAdministratorRepository _administratorRepository;
        public AdministratorController(IAdministratorRepository administratorRepository, IBusinessCardRepository businessCardRepository, ICompanyRepository companyRepository, IManagerRepository managerRepository, ILogger<AdministratorController> logger, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _managerRepository = managerRepository;
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
            _companyRepository = companyRepository;
            _businessCardRepository = businessCardRepository;
            _administratorRepository = administratorRepository;
        }

        [HttpGet("managers")]
        public IActionResult GetManagers()
        {
            var managers = _managerRepository.GetManagers();
            return Ok(Mapper.Map<IEnumerable<EmployeeDto>>(managers));
        }

        [HttpGet("employees")]
        public IActionResult GetEmployees()
        {
            var managers = _businessCardRepository.GetBusinessCardsWithApprovedStatus();
            return Ok(Mapper.Map<IEnumerable<BusinessCardDto>>(managers));
        }

        [HttpDelete("managers/{businessCardId}")]
        public IActionResult DeleteManager(int businessCardId)
        {
            var user = _businessCardRepository.GetApplicationUser(businessCardId);
            if (user == null)
            {
                return NotFound("User does not exist.");
            }

            _administratorRepository.MakeManagerToEmployee(user);
            if (!_administratorRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        [HttpPost("managers/{businessCardId}")]
        public IActionResult MakeManager(int businessCardId)
        {
            var user = _businessCardRepository.GetApplicationUser(businessCardId);
            if (user == null)
            {
                return NotFound("User does not exist.");
            }

            _administratorRepository.MakeManager(user);
            if (!_administratorRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return Ok(null);
        }
    }
}