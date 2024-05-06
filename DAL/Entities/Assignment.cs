using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{

    public class Assignment
    {

        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? InstallationDate { get; set; }
        public double? CustomerNeedsToPay { get; set; }
        public double? CustomerAlreadyPaid { get; set; }
        public double AssignmentCost { get; set; }
        public double? InstallationPrice { get; set; }
        public double? InnerFloorPrice { get; set; }
        public double? OuterFloorPrice { get; set; }
        public double? CarryPrice { get; set; }
        public string Status { get; set; } = "חדש";
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public Guid ManagerId { get; set; }
        public Guid InstallerId { get; set; }
        public Product Product { get; set; }
        public Customer Customer { get; set; }
        public Manager Manager { get; set; }
        public Installer Installer { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
