using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Vms
{
    public class ServiceProviderStockAuditVm
    {
        public Guid Id { get; set; }
        public Guid ServiceProviderIdExternal { get; set; }
        public Guid ServiceProductId { get; set; }
        public int Delta { get; set; }           // +in / -out
        public int BalanceAfter { get; set; }
        public string? Reason { get; set; }
        public DateTime PerformedAt { get; set; }
    }
}
