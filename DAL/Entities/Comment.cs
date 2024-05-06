using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Content { get; set; }
        public Guid WorkerId { get; set; }
        public int AssignmentId { get; set; }
    }
}
