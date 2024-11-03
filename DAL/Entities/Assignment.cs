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
        public CompanyNames CompanyName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? AssignmentDate { get; set; }
        public double? CustomerNeedsToPay { get; set; }
        public double? CustomerAlreadyPaid { get; set; }
        public double? Extras { get; set; }
        public double Cost { get; set; }
        public double? Price { get; set; } //will be used to calaulate profits for the company
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; } = new Guid();
        public Guid EmployeeId { get; set; }
        public Guid ServiceProviderIdExt { get; set; }
        public Guid? MarketerId { get; set; }
        public Product Product { get; set; }
        public Customer Customer { get; set; }
        public Marketer? Marketer { get; set; }

        public List<AssignmentAdditionalPrice> AssignmentAdditionalPrices { get; set; } = new List<AssignmentAdditionalPrice>();
        public List<Comment>? Comments { get; set; } = new List<Comment>();

        public AssignmentStatus Status { get; set; }
        public PickupStatus? PickupStatus { get; set; }
    }
}
