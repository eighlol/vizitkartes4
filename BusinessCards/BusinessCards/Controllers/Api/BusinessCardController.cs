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

namespace BusinessCards.Controllers.Api
{

    [Route("api/businessCards")]
    public class BusinessCardController : Controller
    {
        private readonly IBusinessCardRepository _businessCardRepository;
        private readonly ILogger<BusinessCardController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public BusinessCardController(IBusinessCardRepository businessCardRepository, ILogger<BusinessCardController> logger, UserManager<ApplicationUser> userManager)
        {
            _businessCardRepository = businessCardRepository;
            _logger = logger;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet("my")]
        public IActionResult GetMyBusinessCard()
        {
            var userId = _userManager.GetUserId(User);
            var businessCard = _businessCardRepository.GetBusinessCard(userId);
            if (businessCard == null)
            {
                _logger.LogInformation("Business card wasn't found.");
                return NotFound("Business card not found!");
            }

            var businessCardDto = Mapper.Map<BusinessCardDto>(businessCard);
            return Ok(businessCardDto);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetBusinessCard(int id)
        {
            var businessCard = _businessCardRepository.GetBusinessCard(id);
            if (businessCard == null)
            {
                _logger.LogInformation("Business card wasn't found.");
                return NotFound("Business card not found!");
            }

            var businessCardDto = Mapper.Map<BusinessCardDto>(businessCard);
            return Ok(businessCardDto);
        }

        [HttpGet()]
        public IActionResult GetBusinessCards()
        {
            var businessCards = _businessCardRepository.GetBusinessCards();
            return Ok(Mapper.Map<IEnumerable<BusinessCardDto>>(businessCards));
        }


        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost()]
        public async Task<IActionResult> CreateBusinessCard([FromBody]BusinessCardDto businessCardRequest)
        {
            var user = await _userManager.GetUserAsync(User);
            if (_businessCardRepository.GetBusinessCard(user.Id) != null)
            {
                return BadRequest("Business card already exists!");
            }

            var businessCard = Mapper.Map<BusinessCard>(businessCardRequest);
            businessCard.User = user;
            _businessCardRepository.AddBusinessCard(businessCard);

            if (!_businessCardRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return Ok(Mapper.Map<BusinessCardDto>(businessCard));
        }


        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateBusinessCard(int id, [FromBody] BusinessCardForUpdateDto businessCardDto)
        {
            //var user = await _userManager.GetUserAsync(User);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var businessCard = _businessCardRepository.GetBusinessCard(id);
            if (businessCard == null)
            {
                return NotFound("Business card does not exist!");
            }

            Mapper.Map(businessCardDto, businessCard);
            _businessCardRepository.Update(businessCard);
            if (!_businessCardRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();

        }
    }

}