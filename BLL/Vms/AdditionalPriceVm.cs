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
        public Guid Id { get; set; }
        public double Price { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public Guid AdditionalId { get; set; }
        public Additional? Additional { get; set; }
        [JsonProperty("serviceProviderId")]
        public Guid ServiceProviderIdExt { get; set; }
    }
}
