using System.Collections.Generic;
using BusinessCards.Entities;

namespace BusinessCards.Services
{
    public interface IManagerRepository
    {
        void ApproveEmployee(ApplicationUser user);
        void RemoveEmployee(ApplicationUser user);
        IEnumerable<ApplicationUser> GetManagers();
        void AddManager(ApplicationUser manager);
        bool Save();
    }
}