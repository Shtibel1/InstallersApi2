using DAL.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ServiceProviderStock
    {
        public Guid Id { get; set; }
        public Guid ServiceProviderIdExternal { get; set; }
        public Guid ServiceProductId { get; set; }
        public int Amount { get; set; }

        // Navigations
        public ServiceProduct ServiceProduct { get; set; }
    }
}
