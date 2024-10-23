using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class AdditionalPrice
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public double Price { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid AdditionalId { get; set; }
        public Additional Additional { get; set; }
        public Guid ServiceProviderIdExt { get; set; }
        public List<AssignmentAdditionalPrice> AssignmentAdditionalPrices { get; set; } = new List<AssignmentAdditionalPrice>();

    }
}
