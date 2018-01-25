using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessCards.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusinessCards.Services
{

    public class ManagerRepository : IManagerRepository
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public ManagerRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _dbContext = context;
            _userManager = userManager;
        }

        public IEnumerable<ApplicationUser> GetManagers()
        {
            var users = _dbContext.Users
                .Include(i => i.Company)
                .Include(i => i.BusinessCard)
                .Where(user => user.Company != null && user.EmployeeStatus == EmployeeStatus.Manager).ToList();

            return users;
        }

        public void RemoveEmployeeFromCompany(Company company, int businessCardId)
        {
            var businessCard = _dbContext.BusinessCards.Include(card => card.User).FirstOrDefault(card => card.Id.Equals(businessCardId));
            if (businessCard != null && businessCard.User != null)
            {
                company.Employees.Remove(businessCard.User);
                businessCard.User.EmployeeStatus = EmployeeStatus.None;
            }
        }

        public void UpdateEmployee(ApplicationUser user)
        {
            _dbContext.Users.Update(user);
        }

        public bool Save()
        {
            return (_dbContext.SaveChanges() >= 0);
        }
    }

}

