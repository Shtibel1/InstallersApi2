using DAL.Entities;
using DAL.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class ServiceProviderVm 
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("phone")]
        public string? Phone { get; set; }
        [JsonProperty("role")]
        public Role? Role { get; set; }
        [JsonProperty("categories")]
        public List<CategoryVm>? Categories { get; set; }
        [JsonProperty("CompanyNames")]
        public List<CompanyNames> CompanyNames { get; set; }

    }

 
}
