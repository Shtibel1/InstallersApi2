using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product?> GetProductAsync(Guid id);
        Task<Product> UpdateProductAsync(Guid id, Product product);
        Task<Product> CreateProductAsync(Product product);
        Task DeleteProductAsync(Guid id);
        Task<List<Product>> GetProductsByCategoryId(Guid id);
    }
}
