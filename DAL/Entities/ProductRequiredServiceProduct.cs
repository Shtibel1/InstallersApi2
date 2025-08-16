using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ProductRequiredServiceProduct
    {
        public Guid ProductId { get; set; }
        public Guid ServiceProductId { get; set; }
        public int Quantity { get; set; } = 1;

        // Navigations
        public Product Product { get; set; } = null!;
        public ServiceProduct ServiceProduct { get; set; } = null!;
    }
}
