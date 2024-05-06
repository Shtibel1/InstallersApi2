using BLL.Validations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.AuthService
{
    public class SignupModel
    {
        [JsonProperty("name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [JsonProperty("password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [JsonProperty("phone")]
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
        [JsonProperty("email")]
        public string? Email { get; set; }
        [JsonProperty("role")]
        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }
        [JsonProperty("categories")]
        [ConditionalRequired("Role", "Installer")]
        public List<int>? Categories { get; set; }
    }
}
