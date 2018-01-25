using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessCards.Entities;

namespace BusinessCards.Services
{
    public class AdministratorRepository : IAdministratorRepository 
    {
        private readonly ApplicationDbContext _dbContext;

        public AdministratorRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void MakeManager(ApplicationUser user)
        {
            user.EmployeeStatus = EmployeeStatus.Manager;
        }

        public void MakeManagerToEmployee(ApplicationUser user)
        {
            user.EmployeeStatus = EmployeeStatus.Approved;
        }

        public bool Save()
        {
            return (_dbContext.SaveChanges() >= 0);
        }
    }
}
