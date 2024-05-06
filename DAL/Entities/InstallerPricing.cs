using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class InstallerPricing
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public Guid InstallerId { get; set; }
        public Installer Installer { get; set; }
        public double? InstallationPrice { get; set; }
        public double? OuterFloorPrice { get; set; }
        public double? InnerFloorPrice { get; set; }
        public double? CarryPrice { get; set; }
    }
}
