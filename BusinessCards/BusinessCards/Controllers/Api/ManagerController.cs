﻿using System.Collections.Generic;
using System.Threading.Tasks;
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
    [Authorize(Roles = "CompanyManager, Administrator")]
    [Route("api/company")]
    public class ManagerController : Controller
    {
        private readonly IManagerRepository _managerRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IBusinessCardRepository _businessCardRepository;
        private readonly ILogger<ManagerController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManagerController(IBusinessCardRepository businessCardRepository, ICompanyRepository companyRepository, IManagerRepository managerRepository, ILogger<ManagerController> logger, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _managerRepository = managerRepository;
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
            _companyRepository = companyRepository;
            _businessCardRepository = businessCardRepository;
        }

        [HttpPut("{id}/employee")]
        public IActionResult ApproveEmployee(int id, [FromBody] EmployeeForUpdateDto employeeForUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var businessCard = _businessCardRepository.GetBusinessCard(employeeForUpdateDto.BusinessCardId);
            if (businessCard == null)
            {
                return NotFound($"Business card with id {employeeForUpdateDto.BusinessCardId} was not found!");
            }
            var company = _companyRepository.GetCompany(id);
            if (businessCard.User.Company == company && businessCard.User.EmployeeStatus == EmployeeStatus.NotApproved)
            {
                _managerRepository.ApproveEmployee(businessCard.User);
                if (_managerRepository.Save())
                {
                    return NoContent();
                }
                else
                {
                    _logger.LogError($"Failed to add employee with user id {businessCard.User.Id} to company {company.Id}.");
                    return StatusCode(500, "A problem happened while handling your request.");
                }
            }
            return BadRequest();
        }
    }
}