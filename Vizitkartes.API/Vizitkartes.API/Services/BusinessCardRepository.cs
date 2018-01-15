using System.Collections.Generic;
using System.Linq;
using Vizitkartes.API.Entities;

namespace Vizitkartes.API.Services
{
    public class BusinessCardRepository : IBusinessCardRepository
    {
        private readonly VizitkartesContext _context;

        public BusinessCardRepository(VizitkartesContext context)
        {
            _context = context;
        }
        public IEnumerable<BusinessCard> GetBusinessCards()
        {
            return _context.BusinessCards.OrderBy(b => b.Name).ThenBy(tb => tb.Surname).ToList();
        }

        public BusinessCard GetBusinessCard(int businessCardId)
        {
            return _context.BusinessCards.FirstOrDefault(b => b.Id == businessCardId);
        }
    }
}