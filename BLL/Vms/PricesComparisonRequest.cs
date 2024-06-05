using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Vms
{
    public class PricesComparisonRequest
    {
        public List<Guid> ServiceProviderIds { get; set; }
        public Guid ProductId { get; set; } 
    }
}
