using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Vms
{
    public class ShortageVm
    {
        public Guid ServiceProductId { get; set; }
        public string? ServiceProductName { get; set; }     // optional enrichment
        public int Required { get; set; }
        public int Have { get; set; }
        public int Missing { get; set; }
    }
}
