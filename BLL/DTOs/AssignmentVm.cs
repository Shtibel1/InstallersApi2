using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class AssignmentVm
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? InstallationDate { get; set; }
        public double? CustomerNeedsToPay { get; set; }
        public double? CustomerAlreadyPaid { get; set; }
        public double assignmentCost { get; set; }
        public double? InstallationPrice { get; set; }
        public double? InnerFloorPrice { get; set; }
        public double? OuterFloorPrice { get; set; }
        public double? CarryPrice { get; set; }
        public string Status { get; set; }
        public ProductVm Product { get; set; }
        public CustomerVm Customer { get; set; }
        public InstallerDto Installer { get; set; }
        public ManagerVm Manager { get; set; }
        public List<CommentDto> Comments { get; set; }
    }

}
