using BLL.DTOs;
using BLL.Interfaces;
using BLL.Vms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstallersApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionalPriceController : ControllerBase
    {
        private readonly IAdditionalPriceService _additionalPriceService;

        public AdditionalPriceController(IAdditionalPriceService additionalPriceService)
        {
            _additionalPriceService = additionalPriceService;
        }

        // POST: api/AdditionalPrice
        [HttpPost]
        public async Task<ActionResult<List<AdditionalPriceVm>>> Create([FromBody] List<AdditionalPriceVm> additionalPrices)
        {
            var createdAdditionalPrices = await _additionalPriceService.CreateAsync(additionalPrices);
            return CreatedAtAction(nameof(GetBySP), new { SPId = createdAdditionalPrices.First().ServiceProviderIdExt }, createdAdditionalPrices);
        }

        // DELETE: api/AdditionalPrice/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _additionalPriceService.DeleteAsync(id);
            return NoContent();
        }

        // GET: api/AdditionalPrice/{SPId}/{productId}
        [HttpGet("{SPId}/{productId}")]
        public async Task<ActionResult<List<AdditionalPriceVm>>> Get(Guid SPId, Guid productId)
        {
            var additionalPrice = await _additionalPriceService.Get(SPId, productId);
            if (additionalPrice == null)
            {
                return NotFound();
            }
            return Ok(additionalPrice);
        }

        // GET: api/AdditionalPrice/sp/{SPId}
        [HttpGet("{SPId}")]
        public async Task<ActionResult<List<AdditionalPriceVm>>> GetBySP(Guid SPId)
        {
            var additionalPrices = await _additionalPriceService.GetBySP(SPId);
            if (additionalPrices == null)
            {
                return NotFound();
            }
            return Ok(additionalPrices);
        }

        // PUT: api/AdditionalPrice
        [HttpPut]
        public async Task<ActionResult<List<AdditionalPriceVm>>> Update([FromBody] List<AdditionalPriceVm> additionalPrices)
        {
            var updatedAdditionalPrices = await _additionalPriceService.UpdateAsync(additionalPrices);
            return Ok(updatedAdditionalPrices);
        }
    }

}
