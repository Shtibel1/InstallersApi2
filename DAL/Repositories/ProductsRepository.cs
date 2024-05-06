using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly DataContext _context;

        public ProductsRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Product> GetProductAsync(int id)
        {
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product> CreateProductAsync(Product product) 
        {
            var result = await _context.Products.AddAsync(product).ConfigureAwait(false); 
            await _context.SaveChangesAsync();
            var newProd = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == product.Id);
            return newProd;
        }
        public async Task<Product> UpdateProductAsync(int id, Product product)
        {
            product.Id = id;
            var updatedProd = _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return await GetProductAsync(id);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            
        }

        public async Task<List<Product>> GetProductsByCategoryId(int id)
        {
            return await _context.Products.Where(p => p.CategoryId == id).ToListAsync();
        }


    }
}
