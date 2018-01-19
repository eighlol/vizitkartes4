using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessCards.Entities;

namespace BusinessCards.Services
{
    public interface IBusinessCardRepository
    {
        IEnumerable<BusinessCard> GetBusinessCards();
        BusinessCard GetBusinessCard(int businessCardId);
        void AddBusinessCard(BusinessCard businessCard);
        BusinessCard GetBusinessCard(string userId);
        bool Save();
        void Update(BusinessCard businessCard);
    }
}
