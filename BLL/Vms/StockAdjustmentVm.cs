using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Vms
{
    public class StockAdjustmentVm
    {
        public Guid ServiceProviderIdExternal { get; set; }
        public Guid ServiceProductId { get; set; }
        public int Delta { get; set; }                      // +in / -out (non-zero)
        public Guid? PerformedByUserId { get; set; }
        public Guid? ReferenceId { get; set; }
        public string? Reason { get; set; }
    }
}
