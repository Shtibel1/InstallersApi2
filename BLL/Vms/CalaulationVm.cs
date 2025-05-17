using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Vms
{
    public class CalaulationVm
    {
        public Guid? Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<Guid> AssignmentIds { get; set; }
        public List<AssignmentVm>? Assignments { get; set; }
        public ServiceProviderVm? ServiceProvider { get; set; }
        public Guid ServiceProviderId { get; set; }
    }
}
