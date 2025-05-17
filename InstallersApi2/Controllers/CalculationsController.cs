using BLL.Interfaces;
using BLL.Vms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstallersApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculationsController : ControllerBase
    {
        private readonly ICalaulationsService _calaulationsService;

        public CalculationsController(ICalaulationsService calaulationsService)
        {
            _calaulationsService = calaulationsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _calaulationsService.Get();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _calaulationsService.Get(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CalaulationVm calaulationVm)
        {
            await _calaulationsService.Create(calaulationVm);
            return Ok();
        }
    }
}
