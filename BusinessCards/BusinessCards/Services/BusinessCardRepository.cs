using System.Collections.Generic;
using System.Linq;
using BusinessCards.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessCards.Services
{
    public class BusinessCardRepository : IBusinessCardRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public BusinessCardRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<BusinessCard> GetBusinessCards()
        {
            return _dbContext.BusinessCards.OrderBy(b => b.Name).ThenBy(tb => tb.Surname).ToList();
        }

        public BusinessCard GetBusinessCard(int businessCardId)
        {
            return _dbContext.BusinessCards
                .Include(card => card.User)
                .Include(card => card.User.Company)
                .FirstOrDefault(b => b.Id == businessCardId);
        }

        public BusinessCard GetBusinessCard(string userId)
        {
            return _dbContext.BusinessCards.FirstOrDefault(b => b.UserId == userId);
        }

        public void AddBusinessCard(BusinessCard businessCard)
        {
            _dbContext.BusinessCards.Add(businessCard);
        }

        public bool Save()
        {
            return (_dbContext.SaveChanges() >= 0);
        }

        public void Update(BusinessCard businessCard)
        {
            _dbContext.Update(businessCard);
        }
    }
}