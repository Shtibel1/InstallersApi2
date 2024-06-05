using BLL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class ServiceProviderPricingVm
    {
        [JsonProperty("productId")]
        public Guid ProductId { get; set; }
        [JsonProperty("installerId")]
        public Guid InstallerId { get; set; }
        [JsonProperty("installationPrice")]
        public double? InstallationPrice { get; set; }
        [JsonProperty("outerFloorPrice")]
        public double? OuterFloorPrice { get; set; }
        [JsonProperty("innerFloorPrice")]
        public double? InnerFloorPrice { get; set; }
        [JsonProperty("carryPrice")]
        public double? CarryPrice { get; set; }
        [JsonProperty("distancePrice")]
        public double? DistancePrice { get; set; }

    }
}
