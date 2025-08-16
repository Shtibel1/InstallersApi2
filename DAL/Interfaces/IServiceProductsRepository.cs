using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public record ProductRequirement(Guid ServiceProductId, int Quantity);

    public interface IServiceProductsRepository
    {
        Task<List<ServiceProduct>> ListServiceProductsAsync(CancellationToken ct = default);
        Task<ServiceProduct?> GetServiceProductAsync(Guid id, CancellationToken ct = default);
        Task<ServiceProduct> CreateServiceProductAsync(ServiceProduct sp, CancellationToken ct = default);
        Task UpdateServiceProductAsync(ServiceProduct sp, CancellationToken ct = default);
        Task DeleteServiceProductAsync(Guid id, CancellationToken ct = default);

        // Requirements per product (many-to-many with Quantity)
        Task<IReadOnlyList<ProductRequiredServiceProduct>> GetRequirementsAsync(Guid productId, CancellationToken ct = default);
        Task UpsertRequirementAsync(Guid productId, Guid serviceProductId, int quantity, CancellationToken ct = default);
        Task SetAllRequirementsAsync(Guid productId, IEnumerable<ProductRequirement> requirements, CancellationToken ct = default);
        Task RemoveRequirementAsync(Guid productId, Guid serviceProductId, CancellationToken ct = default);

        // Stock per provider
        Task<int> GetStockAmountAsync(Guid providerId, Guid serviceProductId, CancellationToken ct = default);
        Task<IReadOnlyList<ServiceProviderStock>> GetProviderStockAsync(Guid providerId, CancellationToken ct = default);
        Task<List<ServiceProviderStockAudit>> GetServiceProviderStockAuditsAsync(Guid providerId, Guid serviceProductId, CancellationToken ct = default);
        Task AdjustStockAsync(Guid providerId, Guid serviceProductId, int delta,
                              Guid? performedByUserId = null, Guid? referenceId = null, string? reason = null,
                              CancellationToken ct = default);

        // Helper: can provider install product? (shortages list)
        Task<IReadOnlyList<(Guid ServiceProductId, int Required, int Have, int Missing)>> GetShortagesAsync(
            Guid providerId, Guid productId, CancellationToken ct = default);
    }
}
