using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class CategoryVm
    {
        [JsonProperty("id")]
        public Guid? Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

    }
}
