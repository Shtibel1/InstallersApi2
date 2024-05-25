using DAL.Abstracts;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ServiceProvider> serviceProviders { get; set; }
        public List<Employee> Employee{ get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Logo { get; set; }
    }
}
