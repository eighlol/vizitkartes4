using System.Collections.Generic;
using System.Linq;
using Vizitkartes.API.Entities;

namespace Vizitkartes.API.Services
{
    public class BusinessCardRepository : IBusinessCardRepository
    {
        private readonly VizitkartesContext _dbContextSeedData;

        public BusinessCardRepository(VizitkartesContext dbContextSeedData)
        {
            _dbContextSeedData = dbContextSeedData;
        }
        public IEnumerable<BusinessCard> GetBusinessCards()
        {
            return _dbContextSeedData.BusinessCards.OrderBy(b => b.Name).ThenBy(tb => tb.Surname).ToList();
        }

        public BusinessCard GetBusinessCard(int businessCardId)
        {
            return _dbContextSeedData.BusinessCards.FirstOrDefault(b => b.Id == businessCardId);
        }

        public BusinessCard GetBusinessCard(string userId)
        {
            return _dbContextSeedData.BusinessCards.FirstOrDefault(b => b.UserId == userId);
        }

        public void AddBusinessCard(BusinessCard businessCard)
        {
            _dbContextSeedData.BusinessCards.Add(businessCard);
        }

        public bool Save()
        {
            return (_dbContextSeedData.SaveChanges() >= 0);
        }
    }
}