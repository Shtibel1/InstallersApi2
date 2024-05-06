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
        public int Id { get; set; } = 0;

        [JsonProperty("date")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("customerNeedsToPay")]
        public double? CustomerNeedsToPay { get; set; }
        [JsonProperty("assignmentCost")]
        public double AssignmentCost { get; set; }
        [JsonProperty("installationPrice")]
        public double? InstallationPrice { get; set; }
        [JsonProperty("innerFloorPrice")]
        public double? InnerFloorPrice { get; set; }
        [JsonProperty("outerFloorPrice")]
        public double? OuterFloorPrice { get; set; }
        [JsonProperty("carryPrice")]
        public double? CarryPrice { get; set; }

        [JsonProperty("installerId")]
        public Guid InstallerId { get; set; }
        [JsonProperty("managerId")]
        public Guid ManagerId { get; set; }
        [JsonProperty("productId")]
        public int ProductId { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("customer")]
        public CustomerVm Customer { get; set; }
        [JsonProperty("comments")]
        public List<CommentDto>? Comments { get; set; }
    }

}
