using BLL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class InstallerPricingVm
    {
        public int Id { get; set; }
        [JsonProperty("productId")]
        public int ProductId { get; set; }
        [JsonProperty("installerId")]
        public string InstallerId { get; set; }
        [JsonProperty("installationPrice")]
        public double? InstallationPrice { get; set; }
        [JsonProperty("outerFloorPrice")]
        public double? OuterFloorPrice { get; set; }
        [JsonProperty("innerFloorPrice")]
        public double? InnerFloorPrice { get; set; }
        [JsonProperty("carryPrice")]
        public double? CarryPrice { get; set; }
    }
}
