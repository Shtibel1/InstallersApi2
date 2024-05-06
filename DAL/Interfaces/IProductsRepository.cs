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
        Task<Product> GetProductAsync(int id);
        Task<Product> UpdateProductAsync(int id, Product product);
        Task<Product> CreateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<List<Product>> GetProductsByCategoryId(int id);
    }
}
