using DAL.Data;
using DAL.Entities;
using DAL.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Providers
{
    public class CompanyDataProvider : ICompanyDataProvider
    {
        private readonly IConfiguration _configuration;
        private readonly ConcurrentDictionary<CompanyNames, CompanyDataDto> _companyDataDictionary;

        public CompanyDataProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            _companyDataDictionary = new ConcurrentDictionary<CompanyNames, CompanyDataDto>();
        }

        public void SetCompanies(List<CompanyNames>? companies)
        {
            if (companies == null || companies.Count == 0)
                return;


            foreach (var company in companies)
            {
                var connectionString = _configuration.GetValue<string>($"ConnectionStrings:{company}");
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException($"Connection string for company {company} is missing.");
                }

                var context = new CompanyDbContext(connectionString);
                _companyDataDictionary[company] = new CompanyDataDto(connectionString, context);
            }
        }

        public List<string> GetConnectionStrings()
        {
            return _companyDataDictionary.Values.Select(c => c.ConnectionString).ToList();
        }

        public List<CompanyNames> GetCompanies()
        {
            return _companyDataDictionary.Keys.ToList();
        }

        public List<CompanyDbContext> GetContexts()
        {
            return _companyDataDictionary.Values.Select(c => c.Context).ToList();
        }

        public CompanyDbContext GetContext(CompanyNames company)
        {
            if (!_companyDataDictionary.ContainsKey(company))
            {
                throw new KeyNotFoundException($"No context found for company {company}");
            }
            return _companyDataDictionary[company].Context;
        }

    }
}
