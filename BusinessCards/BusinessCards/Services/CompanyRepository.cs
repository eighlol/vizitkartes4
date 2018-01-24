using System.Collections.Generic;
using System.Linq;
using BusinessCards.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessCards.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CompanyRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public IEnumerable<Company> GetCompanies()
        {
            return _dbContext.Companies.Include(company => company.Employees).OrderBy(b => b.Name).ToList();
        }

        public Company GetUserCompany(ApplicationUser user)
        {
            return _dbContext.Users.FirstOrDefault(u => u == user)?.Company;
        }

        public Company GetCompany(int id)
        {
            return _dbContext.Companies.FirstOrDefault(company => company.Id == id);
        }

        public IEnumerable<BusinessCard> GetCompanyEmployees(Company company)
        {
            return _dbContext.Users.Where(user => user.Company == company).Select(user => user.BusinessCard).ToList();
        }

        public void MakeCompanyEmployee(ApplicationUser user, int companyId)
        {
            user.EmployeeStatus = EmployeeStatus.NotApproved;
            var company = GetCompany(companyId);
            company.Employees.Add(user);
        }

        public bool Save()
        {
            return (_dbContext.SaveChanges() >= 0);
        }
    }
}