using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ServiceProviderStockAudit
    {
        public Guid Id { get; set; }
        public Guid ServiceProviderIdExternal { get; set; }
        public Guid ServiceProductId { get; set; }
        public int Delta { get; set; }           // +in / -out
        public int BalanceAfter { get; set; }
        public string? Reason { get; set; }
        public Guid? PerformedByUserId { get; set; }
        public Guid? ReferenceId { get; set; }   // e.g., AssignmentId/OrderId
        public DateTime PerformedAt { get; set; }

        // Navigations
        public ServiceProduct ServiceProduct { get; set; } = null!;
    }
}
