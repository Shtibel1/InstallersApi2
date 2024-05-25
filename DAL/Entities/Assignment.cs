using DAL.Abstracts;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{

    public class Assignment
    {
        public Guid Id { get; set; }
        public CompanyNames companyName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? AssignmentDate { get; set; }
        public double? CustomerNeedsToPay { get; set; }
        public double? CustomerAlreadyPaid { get; set; }
        public double Cost { get; set; }
        public double? Price { get; set; }
        public AssignmentStatus Status { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid ServiceProviderId { get; set; }
        public Guid? MarketerId { get; set; }
        public Product Product { get; set; }
        public Customer Customer { get; set; }
        public List<Comment>? Comments { get; set; } = new List<Comment>();
        public PickupStatus? PickupStatus { get; set; }
        public Marketer? Marketer { get; set; }


        public double AssignmentPrice { get; set; }
        public double? InnerFloorPrice { get; set; }
        public double? OuterFloorPrice { get; set; }
        public double? CarryPrice { get; set; }
        public double? DistancePrice { get; set; }
    }
}
