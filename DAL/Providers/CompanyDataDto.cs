using DAL.Data;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Providers
{
    public class CompanyDataDto
    {
        public string ConnectionString { get; }
        public CompanyDbContext Context { get; }

        public CompanyDataDto( string companyConnectionStrings, CompanyDbContext context)
        {
            this.ConnectionString = companyConnectionStrings;
            this.Context = context;
        }
    }
}
