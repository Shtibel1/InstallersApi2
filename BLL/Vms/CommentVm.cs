using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class CommentVm
    {
        public Guid? Id { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("userId")]
        public Guid UserId { get; set; }

    }
}
