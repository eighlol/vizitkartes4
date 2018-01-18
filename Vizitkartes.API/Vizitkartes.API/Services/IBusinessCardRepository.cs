using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vizitkartes.API.Entities;

namespace Vizitkartes.API.Services
{
    public interface IBusinessCardRepository
    {
        IEnumerable<BusinessCard> GetBusinessCards();
        BusinessCard GetBusinessCard(int businessCardId);
        void AddBusinessCard(BusinessCard businessCard);
        BusinessCard GetBusinessCard(string userId);
        bool Save();

    }
}
