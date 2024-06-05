using DAL.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class CreateAssignmentVm
    {
        [JsonProperty("id")]
        public Guid? Id { get; set; } = Guid.NewGuid();

        [JsonProperty("date")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("customerNeedsToPay")]
        public double? CustomerNeedsToPay { get; set; }
        [JsonProperty("assignmentCost")]
        public double Cost { get; set; }
        [JsonProperty("installationPrice")]
        public double AssignmentPrice { get; set; }
        [JsonProperty("innerFloorPrice")]
        public double? InnerFloorPrice { get; set; }
        [JsonProperty("outerFloorPrice")]
        public double? OuterFloorPrice { get; set; }
        [JsonProperty("carryPrice")]
        public double? CarryPrice { get; set; }
        [JsonProperty("distancePrice")]
        public double? DistancePrice { get; set; }

        [JsonProperty("serviceProviderId")]
        public Guid ServiceProviderId { get; set; }
        [JsonProperty("employeeId")]
        public Guid EmployeeId { get; set; }
        [JsonProperty("productId")]
        public Guid ProductId { get; set; }
        [JsonProperty("status")]
        public AssignmentStatus Status { get; set; } = AssignmentStatus.New;
        [JsonProperty("customer")]
        public CustomerVm Customer { get; set; }
        [JsonProperty("comments")]
        public List<CommentVm>? Comments { get; set; }
        [JsonProperty("pickupStatus")]
        public CompanyNames? CompanyName { get; set; }

        public Guid MarketerId { get; set; }
    }

}
