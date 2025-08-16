using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Vms
{
    public class ProductRequirementVm
    {
        public Guid ServiceProductId { get; set; }
        public string? ServiceProductName { get; set; } // filled from navigation when available
        public int Quantity { get; set; }
    }
}
