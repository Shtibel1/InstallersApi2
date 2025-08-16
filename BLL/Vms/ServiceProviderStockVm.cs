using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Vms
{
    public class ServiceProviderStockVm
    {
        public Guid Id { get; set; }
        public Guid ServiceProviderIdExternal { get; set; }
        public Guid ServiceProductId { get; set; }
        public string? ServiceProductName { get; set; } // filled from navigation when available
        public int Amount { get; set; }

        public List<ServiceProviderStockAuditVm> AuditVm { get; set; }
    }
}
