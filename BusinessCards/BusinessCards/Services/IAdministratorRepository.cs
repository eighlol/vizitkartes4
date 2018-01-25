using System.Collections.Generic;
using BusinessCards.Entities;

namespace BusinessCards.Services
{
    public interface IAdministratorRepository
    {
        void MakeManager(ApplicationUser user);
        void MakeManagerToEmployee(ApplicationUser user);
        bool Save();
    }
}