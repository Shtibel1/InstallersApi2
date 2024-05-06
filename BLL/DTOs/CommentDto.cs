using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class CommentDto
    {
        public int? Id { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }

    }
}
