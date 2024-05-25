using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class AdditionalsVm
    {
        [JsonProperty("innerFloorPrice")]
        public double? InnerFloorPrice { get; set; }

        [JsonProperty("outerFloorPrice")]
        public double? OuterFloorPrice { get; set; }

        [JsonProperty("carryPrice")]
        public double? CarryPrice { get; set; }
        [JsonProperty("distancePrice")]
        public double? DistancePrice { get; set; }
    }
}
