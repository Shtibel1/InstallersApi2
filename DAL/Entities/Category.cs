using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ServiceProviderCategory> ServiceProviderCategories { get; set; } = new List<ServiceProviderCategory>();
    }
}
