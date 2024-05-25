using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Position { get; set; }
        public double? CustomerAssignmentPrice { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
