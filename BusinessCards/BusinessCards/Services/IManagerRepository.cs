using System.Collections.Generic;
using BusinessCards.Entities;

namespace BusinessCards.Services
{
    public interface IManagerRepository
    {
        void UpdateEmployee(ApplicationUser user);
        IEnumerable<ApplicationUser> GetManagers();
        void RemoveEmployeeFromCompany(Company company, int businessCardId);
        bool Save();
    }
}