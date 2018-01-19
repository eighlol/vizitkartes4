using System.Collections.Generic;
using BusinessCards.Entities;

namespace BusinessCards.Services
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetCompanies();
        Company GetUserCompany(ApplicationUser user);
        Company GetCompany(int id);
        IEnumerable<BusinessCard> GetCompanyEmployees(Company company);
        void MakeCompanyEmployee(ApplicationUser user, int companyId);
        bool Save();
    }
}