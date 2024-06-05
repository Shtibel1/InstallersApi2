using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class ProductVm
    {
        public Guid Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("place")]
        public int? Position { get; set; }
        [JsonProperty("customerInstallationPrice")]
        public double? CustomerInstallationPrice { get; set; }
        [JsonProperty("categoryId")]
        public Guid CategoryId { get; set; }
        [JsonProperty("category")]
        public CategoryVm? Category { get; set; }
    }
}
