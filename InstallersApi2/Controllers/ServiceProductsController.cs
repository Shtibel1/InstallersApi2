using BLL.Interfaces;
using BLL.Services;
using BLL.Vms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstallersApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProductsController : ControllerBase
    {
        private readonly IServiceProductsService _svc;
        public ServiceProductsController(IServiceProductsService serviceProductsService)
        {
            _svc = serviceProductsService;
        }

        [HttpGet("service-products")]
        public async Task<ActionResult<IReadOnlyList<ServiceProductVm>>> ListServiceProducts(CancellationToken ct)
        => Ok(await _svc.ListServiceProductsAsync(ct));

        [HttpGet("service-products/{id:guid}")]
        public async Task<ActionResult<ServiceProductVm>> GetServiceProduct(Guid id, CancellationToken ct)
        {
            var item = await _svc.GetServiceProductAsync(id, ct);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost("service-products")]
        public async Task<ActionResult<ServiceProductVm>> CreateServiceProduct([FromBody] ServiceProductVm vm, CancellationToken ct)
        {
            if (vm is null) return BadRequest("Body is required.");
            var created = await _svc.CreateServiceProductAsync(vm, ct);
            return CreatedAtAction(nameof(GetServiceProduct), new { id = created.Id }, created);
        }

        [HttpPut("service-products/{id:guid}")]
        public async Task<IActionResult> UpdateServiceProduct(Guid id, [FromBody] ServiceProductVm vm, CancellationToken ct)
        {
            if (vm is null) return BadRequest("Body is required.");
            vm.Id = id;
            await _svc.UpdateServiceProductAsync(vm, ct);
            return NoContent();
        }

        [HttpDelete("service-products/{id:guid}")]
        public async Task<IActionResult> DeleteServiceProduct(Guid id, CancellationToken ct)
        {
            await _svc.DeleteServiceProductAsync(id, ct);
            return NoContent();
        }

        // ===== Product Requirements (many-to-many with Quantity) ==================

        [HttpGet("products/{productId:guid}/requirements")]
        public async Task<ActionResult<IReadOnlyList<ProductRequirementVm>>> GetRequirements(Guid productId, CancellationToken ct)
            => Ok(await _svc.GetRequirementsAsync(productId, ct));

        [HttpPut("products/{productId:guid}/requirements")]
        public async Task<IActionResult> SetAllRequirements(Guid productId, [FromBody] IEnumerable<ProductRequirementVm> reqs, CancellationToken ct)
        {
            await _svc.SetAllRequirementsAsync(productId, reqs ?? Enumerable.Empty<ProductRequirementVm>(), ct);
            return NoContent();
        }

        [HttpPut("products/{productId:guid}/requirements/{serviceProductId:guid}")]
        public async Task<IActionResult> UpsertRequirement(Guid productId, Guid serviceProductId, [FromQuery] int quantity, CancellationToken ct)
        {
            await _svc.UpsertRequirementAsync(productId, serviceProductId, quantity, ct);
            return NoContent();
        }

        [HttpDelete("products/{productId:guid}/requirements/{serviceProductId:guid}")]
        public async Task<IActionResult> RemoveRequirement(Guid productId, Guid serviceProductId, CancellationToken ct)
        {
            await _svc.RemoveRequirementAsync(productId, serviceProductId, ct);
            return NoContent();
        }

        // ===== Provider Stock (+ audit) ==========================================

        [HttpGet("providers/{providerId:guid}/stock")]
        public async Task<ActionResult<IReadOnlyList<ServiceProviderStockVm>>> GetProviderStock(Guid providerId, CancellationToken ct)
            => Ok(await _svc.GetProviderStockAsync(providerId, ct));

        [HttpGet("providers/{providerId:guid}/stock/{serviceProductId:guid}")]
        public async Task<ActionResult<int>> GetStockAmount(Guid providerId, Guid serviceProductId, CancellationToken ct)
            => Ok(await _svc.GetStockAmountAsync(providerId, serviceProductId, ct));

        [HttpPost("providers/stock/adjust")]
        public async Task<IActionResult> AdjustStock([FromBody] StockAdjustmentVm adj, CancellationToken ct)
        {
            if (adj is null) return BadRequest("Body is required.");
            await _svc.AdjustStockAsync(adj, ct);
            return NoContent();
        }

        // ===== Helper: shortages (can provider install product?) ==================

        [HttpGet("providers/{providerId:guid}/shortages/{productId:guid}")]
        public async Task<ActionResult<IReadOnlyList<ShortageVm>>> GetShortages(
            Guid providerId, Guid productId, [FromQuery] bool includeNames = false, CancellationToken ct = default)
            => Ok(await _svc.GetShortagesAsync(providerId, productId, includeNames, ct));
    }
}
