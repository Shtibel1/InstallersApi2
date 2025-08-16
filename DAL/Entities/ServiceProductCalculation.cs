using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ServiceProductCalculation
    {
        public Guid Id { get; set; }
        public Guid ServiceProductId { get; set; }
        public ServiceProduct ServiceProduct { get; set; }
        public int Amount { get; set; }
        public double? Price { get; set; }
    }
}
