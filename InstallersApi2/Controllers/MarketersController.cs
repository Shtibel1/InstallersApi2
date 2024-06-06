using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstallersApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class MarketersController : ControllerBase
    {
        private readonly IMarketersService _marketersService;

        public MarketersController(IMarketersService marketersService)
        {
            _marketersService = marketersService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMarketer([FromBody] MarketerVm marketer)
        {
            if (marketer == null)
            {
                return BadRequest();
            }

            var createdMarketer = await _marketersService.CreateMarketerAsync(marketer);
            return CreatedAtAction(nameof(GetMarketer), new { id = createdMarketer.Id }, createdMarketer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarketer(Guid id)
        {
            var marketer = await _marketersService.GetMarketerAsync(id);
            if (marketer == null)
            {
                return NotFound();
            }

            await _marketersService.DeleteMarketerAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMarketer(Guid id)
        {
            var marketer = await _marketersService.GetMarketerAsync(id);
            if (marketer == null)
            {
                return NotFound();
            }

            return Ok(marketer);
        }

        [HttpGet]
        public async Task<IActionResult> GetMarketers()
        {
            var marketers = await _marketersService.GetMarketersAsync();
            return Ok(marketers);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMarketer(Guid id, [FromBody] MarketerVm marketer)
        {
            if (marketer == null || marketer.Id != id)
            {
                return BadRequest();
            }

            var existingMarketer = await _marketersService.GetMarketerAsync(id);
            if (existingMarketer == null)
            {
                return NotFound();
            }

            await _marketersService.UpdateMarketerAsync(marketer);
            return NoContent();
        }
    }
}
