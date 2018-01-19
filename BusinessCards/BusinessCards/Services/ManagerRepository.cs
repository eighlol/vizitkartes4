using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessCards.Entities;
using Microsoft.AspNetCore.Identity;

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
            var users = _dbContext.Users.Where(user => user.Company != null).ToList();

            return users;
            //return _dbContext.Managers.OrderBy(manager => manager.UserName).ToList();
        }

        public void ApproveEmployee(ApplicationUser user)
        {
            var employeeStatus = EmployeeStatus.Approved;
            var dbUser = _dbContext.Users.FirstOrDefault(applicationUser => applicationUser == user);
            dbUser.EmployeeStatus = EmployeeStatus.Approved;
        }

        public void RemoveEmployee(ApplicationUser user)
        {
            throw new System.NotImplementedException();
        }

        public void AddManager(ApplicationUser manager)
        {
            //_dbContext.Managers.Add(manager);
            //_dbContext.SaveChanges();
        }
        public bool Save()
        {
            return (_dbContext.SaveChanges() >= 0);
        }
    }

}

