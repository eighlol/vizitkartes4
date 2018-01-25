using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessCards.Entities;
using BusinessCards.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace BusinessCards.Controllers.Api
{
    [Authorize]
    [Route("api/identification")]
    public class IdentificationController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IBusinessCardRepository _businessCardRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public IdentificationController(ICompanyRepository companyRepository, UserManager<ApplicationUser> userManager,IBusinessCardRepository businessCardRepository)
        {
            _companyRepository = companyRepository;
            _userManager = userManager;
            _businessCardRepository = businessCardRepository;
        }

        [HttpGet("whoAmI")]
        public async Task<IActionResult> WhoAmI()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var userCompany = _companyRepository.GetUserCompany(user);

            var businessCard = _businessCardRepository.GetBusinessCard(user.Id);

            var res = new WhoAmIDto
            {
                BusinessCardId = businessCard?.Id,
                CompanyId = userCompany?.Id,
                EmployeeStatus = user.EmployeeStatus == EmployeeStatus.NotApproved ? "Not approved" : user.EmployeeStatus.ToString()
            };

            return Ok(res);
        }

    }
}