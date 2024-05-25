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
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("place")]
        public int? Position { get; set; }
        [JsonProperty("customerInstallationPrice")]
        public double? CustomerInstallationPrice { get; set; }
        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }
        [JsonProperty("category")]
        public CategoryVm? Category { get; set; }
    }
}
