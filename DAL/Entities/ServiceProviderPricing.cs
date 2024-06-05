using DAL.Abstracts;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ServiceProviderPricing
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid InstallerId { get; set; }
        public Guid ServiceProviderIdExternal { get; set; }
        public double InstallationPrice { get; set; }
        public double? InnerFloorPrice { get; set; }
        public double? OuterFloorPrice { get; set; }
        public double? CarryPrice { get; set; }
        public double? DistancePrice { get; set; }
        public double? DeliveryOnlyPrice { get; set; }
    }
}
