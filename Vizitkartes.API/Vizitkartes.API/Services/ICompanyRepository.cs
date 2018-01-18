using System.Collections.Generic;
using Vizitkartes.API.Entities;

namespace Vizitkartes.API.Services
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetCompanies();

        Company GetCompany(int id);
        IEnumerable<BusinessCard> GetCompanyEmployees(Company company);
    }
}