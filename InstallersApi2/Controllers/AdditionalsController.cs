using BLL.DTOs;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstallersApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionalsController : ControllerBase
    {
        private readonly ILogger<AdditionalsController> _logger;
        private readonly IAdditionalsService _additionalsService;

        public AdditionalsController(ILogger<AdditionalsController> logger, IAdditionalsService additionalsService)
        {
            _logger = logger;
            _additionalsService = additionalsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AdditionalVm>>> Get()
        {
            return await _additionalsService.GetAsync();
        }

        [HttpPost]
        public async Task<ActionResult<AdditionalVm>> Post(AdditionalVm additional)
        {
            var newAdditional = await _additionalsService.CreateAsync(additional);
            return CreatedAtAction(nameof(Get), new { id = newAdditional.Id }, newAdditional);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(AdditionalVm additional)
        {
            await _additionalsService.UpdateAsync(additional);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _additionalsService.DeleteAsync(id);
            return NoContent();
        }


    }
}
