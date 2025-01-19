using BLL.DTOs;
using BLL.Vms;
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

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("assignmentDate")]
        public DateTime? AssignmentDate { get; set; }
        [JsonProperty("customerNeedsToPay")]
        public double? CustomerNeedsToPay { get; set; }
        [JsonProperty("cost")]
        public double Cost { get; set; } 
        [JsonProperty("extras")]
        public double Extras { get; set; }        

        [JsonProperty("serviceProviderId")]
        public Guid ServiceProviderId { get; set; }
        [JsonProperty("employeeId")]
        public Guid EmployeeId { get; set; }
        [JsonProperty("productId")]
        public Guid ProductId { get; set; }
        [JsonProperty("status")]
        public AssignmentStatus Status { get; set; } = AssignmentStatus.New;
        [JsonProperty("pickupStatus")]
        public PickupStatus PickupStatus { get; set; } = PickupStatus.NotReady;
        [JsonProperty("customer")]
        public CustomerVm Customer { get; set; }
        [JsonProperty("comments")]
        public List<CommentVm>? Comments { get; set; }
        [JsonProperty("companyName")]
        public CompanyNames? CompanyName { get; set; }

        public Guid MarketerId { get; set; }
        [JsonProperty("additionalPrices")]
        public List<AdditionalPriceVm> AdditionalPrices { get; set; } = new List<AdditionalPriceVm>();

        [JsonProperty("numOfProducts")]
        public int? NumOfProducts { get; set; }
    }

}
