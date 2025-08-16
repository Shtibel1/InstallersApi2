using BLL.Vms;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IServiceProductsService
    {
        Task<IReadOnlyList<ServiceProductVm>> ListServiceProductsAsync(CancellationToken ct = default);
        Task<ServiceProductVm?> GetServiceProductAsync(Guid id, CancellationToken ct = default);
        Task<ServiceProductVm> CreateServiceProductAsync(ServiceProductVm vm, CancellationToken ct = default);
        Task UpdateServiceProductAsync(ServiceProductVm vm, CancellationToken ct = default);
        Task DeleteServiceProductAsync(Guid id, CancellationToken ct = default);

        // Requirements per product (many-to-many with Quantity)
        Task<IReadOnlyList<ProductRequirementVm>> GetRequirementsAsync(Guid productId, CancellationToken ct = default);
        Task UpsertRequirementAsync(Guid productId, Guid serviceProductId, int quantity, CancellationToken ct = default);
        Task SetAllRequirementsAsync(Guid productId, IEnumerable<ProductRequirementVm> requirements, CancellationToken ct = default);
        Task RemoveRequirementAsync(Guid productId, Guid serviceProductId, CancellationToken ct = default);

        // Stock per provider (+ audit)
        Task<int> GetStockAmountAsync(Guid providerId, Guid serviceProductId, CancellationToken ct = default);
        Task<IReadOnlyList<ServiceProviderStockVm>> GetProviderStockAsync(Guid providerId, CancellationToken ct = default);
        Task AdjustStockAsync(StockAdjustmentVm adjustment, CancellationToken ct = default);

        // Helper
        Task<IReadOnlyList<ShortageVm>> GetShortagesAsync(Guid providerId, Guid productId, bool includeNames = false, CancellationToken ct = default);
    }
}
