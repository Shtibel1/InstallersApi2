using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class AssignmentAdditionalPrice
    {
        public Guid AssignmentId { get; set; }
        public Assignment Assignment { get; set; }

        public Guid AdditionalPriceId { get; set; }
        public AdditionalPrice AdditionalPrice { get; set; }
    }
}
