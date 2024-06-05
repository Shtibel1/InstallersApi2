using DAL.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ServiceProviderCategory
    {
        public Guid Id { get; set; }
        public Guid ServiceProviderIdExternal { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
