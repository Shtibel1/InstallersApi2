using DAL.Entities;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class ServiceProviderVm 
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public Role? Role { get; set; }
        public List<CategoryVm>? Categories { get; set; }
        public List<CompanyNames> CompanyNames { get; set; }

    }

 
}
