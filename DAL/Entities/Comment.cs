using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public Guid WorkerId { get; set; }
        public Guid AssignmentId { get; set; }
        public string Content { get; set; }
    }
}
