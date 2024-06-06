using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InstallersApi2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        [HttpPost("AdjustCategoriesToServiceProvider")]
        public async Task<IActionResult> AdjustCategoriesToServiceProvider([FromBody] AddCategoriesToServiceProviderVm addCategoriesToServiceProviderVm)
        {

            var serviceProvider = await _employeesService
                .AddCategoriesToServiceProviderAsync(
                addCategoriesToServiceProviderVm.Categories,
                addCategoriesToServiceProviderVm.ServiceProviderId);

            return StatusCode(201, serviceProvider);
        }
    }

    public class AddCategoriesToServiceProviderVm
    {
        [JsonProperty("categories")]
        public List<CategoryVm> Categories { get; set; }
        [JsonProperty("serviceProviderId")]
        public Guid ServiceProviderId { get; set; }
    }
}
