using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vizitkartes.API.Entities;
using Vizitkartes.API.Models;
using Vizitkartes.API.Services;

namespace Vizitkartes.API.Controllers
{
    [Route("api/businessCards")]
    public class BusinessCardController : Controller
    {
        private readonly IBusinessCardRepository _businessCardRepository;
        private readonly ILogger<BusinessCardController> _logger;
        private readonly UserManager<VizitkartesUser> _userManager;

        public BusinessCardController(IBusinessCardRepository businessCardRepository, ILogger<BusinessCardController> logger, UserManager<VizitkartesUser> userManager)
        {
            _businessCardRepository = businessCardRepository;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet()]
        public IActionResult GetBusinessCards()
        {
            var businessCards = Mapper.Map<IEnumerable<BusinessCardDto>>(_businessCardRepository.GetBusinessCards());
            return Ok(businessCards);
        }

        [HttpGet("{id}")]
        public IActionResult GetBusinessCards(int id)
        {
            var businessCard = _businessCardRepository.GetBusinessCard(id);
            if (businessCard == null)
            {
                _logger.LogInformation($"Business card with id {id} wasn't found.");
                return NotFound();
            }
                
            return Ok(Mapper.Map<BusinessCardDto>(businessCard));

        }

        [HttpPost()]
        public async Task<IActionResult> CreateBusinessCard([FromBody] BusinessCardDto businessCardRequest)
        {
            var user = await  _userManager.GetUserAsync(User);
            if (_businessCardRepository.GetBusinessCard(user.Id) != null)
            {
                return BadRequest("Business card already exists!");
            }

            var businessCard =  Mapper.Map<BusinessCard>(businessCardRequest);
            businessCard.User = user;
            _businessCardRepository.AddBusinessCard(businessCard);

            if (!_businessCardRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return Ok(Mapper.Map<BusinessCardDto>(businessCard));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBusinessCard(int id, [FromBody] BusinessCardDto businessCardDto)
        {
            var user = await _userManager.GetUserAsync(User);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var businessCard = _businessCardRepository.GetBusinessCard(id);
            if  (businessCard == null)
            {
                return NotFound("Business card does not exist!");
            }

            Mapper.Map(businessCardDto, businessCard);
            if (!_businessCardRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();

        }
    }

}