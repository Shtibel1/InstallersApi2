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
        Task<ProductVm> GetProductAsync(int id);
        Task<ProductVm> UpdateProductAsync(int id, ProductVm product);
        Task<ProductVm> CreateProductAsync(ProductVm product);
        Task<SingleProductVm> GetProductWithPrices(int id, Guid installerId, int productId);
        Task DeleteProductAsync(int id);
    }

    public class SingleProductVm
    {
        public ProductVm Product { get; set; }
        public InstallerPricingVm installerPricing { get; set; }
    }
}
