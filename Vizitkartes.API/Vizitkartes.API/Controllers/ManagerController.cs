using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vizitkartes.API.Models;
using Vizitkartes.API.Services;

namespace Vizitkartes.API.Controllers
{
    [Route("api/BusinessCard")]
    public class ManagerController : Controller
    {
        private readonly IBusinessCardRepository _businessCardRepository;
        private readonly ILogger<ManagerController> _logger;

        public ManagerController(IBusinessCardRepository businessCardRepository, ILogger<ManagerController> logger)
        {
            _businessCardRepository = businessCardRepository;
            _logger = logger;
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
    }
}