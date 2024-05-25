using BLL.DTOs;
using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProductsService
    {
        Task<List<ProductVm>> GetProductsAsync();
        Task<ProductVm> GetProductAsync(Guid id);
        Task<ProductVm> UpdateProductAsync(Guid id, ProductVm product);
        Task<ProductVm> CreateProductAsync(ProductVm product);
        Task<SingleProductVm> GetProductWithPrices(Guid id, Guid installerId, Guid productId);
        Task DeleteProductAsync(Guid id);
    }

    public class SingleProductVm
    {
        public ProductVm Product { get; set; }
        public ServiceProviderPricingVm installerPricing { get; set; }
    }
}
