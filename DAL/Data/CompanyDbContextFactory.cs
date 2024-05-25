using DAL.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class CompanyDbContextFactory
    {
        private readonly ICompanyDataProvider _companyDataProvider;

        public CompanyDbContextFactory(ICompanyDataProvider companyDataProvider)
        {
            _companyDataProvider = companyDataProvider;
        }

        
    }
}
