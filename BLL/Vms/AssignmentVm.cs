using BLL.Services;
using DAL.Entities;
using DAL.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class AssignmentVm
    {        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("businessName")]
        public CompanyNames CompanyName { get; set; }
        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("assignmentDate")]
        public DateTime? AssignmentDate { get; set; }

        [JsonProperty("cost")]
        public double Cost { get; set; }

        [JsonProperty("price")]
        public double? Price { get; set; }

        [JsonProperty("status")]
        public AssignmentStatus Status { get; set; }

        [JsonProperty("product")]
        public ProductVm Product { get; set; }

        [JsonProperty("customer")]
        public CustomerVm Customer { get; set; }

        [JsonProperty("serviceProvider")]
        public ServiceProviderVm ServiceProvider { get; set; }

        [JsonProperty("employee")]
        public EmployeeVm Employee { get; set; }

        [JsonProperty("comments")]
        public List<CommentVm> Comments { get; set; }
        [JsonProperty("pickupStatus")]
        public PickupStatus? PickupStatus { get; set; }
        [JsonProperty("customerNeedsToPay")]
        public double? CustomerNeedsToPay { get; set; }

        [JsonProperty("customerAlreadyPaid")]
        public double? CustomerAlreadyPaid { get; set; }


        [JsonProperty("marketer")]
        public MarketerVm Marketer { get; set; }
        [JsonProperty("assignmentPrice")]
        public double AssignmentPrice { get; set; }
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
