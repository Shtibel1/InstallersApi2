using DAL.Data;
using DAL.Entities;
using DAL.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ICompanyDataProvider _companyDataProvider;

        public ProductsRepository(ICompanyDataProvider companyDataProvider)
        {
            _companyDataProvider = companyDataProvider;

        }
        public async Task<Product?> GetProductAsync(Guid id)
        {
            var context = _companyDataProvider.GetContexts()[0];
            return await context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var context = _companyDataProvider.GetContexts()[0];

            return await context.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product> CreateProductAsync(Product product) 
        {
            var _context = _companyDataProvider.GetContexts()[0];
            var result = await _context.Products.AddAsync(product).ConfigureAwait(false); 
            await _context.SaveChangesAsync();
            var newProd = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == product.Id);
            return newProd;
        }
        public async Task<Product> UpdateProductAsync(Guid id, Product product)
        {
            var _context = _companyDataProvider.GetContexts()[0];
            product.Id = id;
            var updatedProd = _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return await GetProductAsync(id);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var _context = _companyDataProvider.GetContexts()[0];
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            
        }

        public async Task<List<Product>> GetProductsByCategoryId(Guid id)
        {
            var _context = _companyDataProvider.GetContexts()[0];
            return await _context.Products.Where(p => p.CategoryId == id).ToListAsync();
        }


    }
}
