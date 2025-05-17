using DAL.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Calculation
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<CalculationAssignment> CalculationAssignments { get; set; }
        public Guid ServiceProviderId { get; set; }
    }
}
