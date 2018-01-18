using System.Collections.Generic;
using System.Linq;
using Vizitkartes.API.Entities;

namespace Vizitkartes.API.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly VizitkartesContext _dbContext;

        public CompanyRepository(VizitkartesContext context)
        {
            _dbContext = context;
        }
        public IEnumerable<Company> GetCompanies()
        {
            return _dbContext.Companies.OrderBy(b => b.Name).ThenBy(tb => tb.Manager.BusinessCard.Name);
        }

        public Company GetCompany(int id)
        {
            return _dbContext.Companies.FirstOrDefault(company => company.Id == id);
        }

        public IEnumerable<BusinessCard> GetCompanyEmployees(Company company)
        {
            throw new System.NotImplementedException();
        }
    }
}