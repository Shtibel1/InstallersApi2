using BLL.DTOs;
using DAL.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Vms
{
    public class AdditionalPriceVm
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("productId")]
        public Guid ProductId { get; set; }
        [JsonProperty("product")]
        public Product? Product { get; set; }
        public Guid AdditionalId { get; set; }
        [JsonProperty("additional")]
        public AdditionalVm? Additional { get; set; }
        [JsonProperty("serviceProviderId")]
        public Guid ServiceProviderIdExt { get; set; }
    }
}
