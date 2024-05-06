using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly DataContext _context;

        public CategoriesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            
            return  await _context.Categories.FindAsync(id);
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            var result = await _context.Categories.AddAsync(category).ConfigureAwait(false);
            if (result != null && result.Entity != null)
            {
                var newCat = result.Entity;
                await _context.SaveChangesAsync();
                return newCat;
            }
            await _context.SaveChangesAsync();
            return null;
        }
        public async Task<Category> UpdateCategoryAsync(int id, Category category)
        {
            var result = _context.Categories.Update(category);
            if (result != null && result.Entity != null)
            {
                var updatedCat = result.Entity;
                await _context.SaveChangesAsync();
                return updatedCat;
            }
            await _context.SaveChangesAsync();
            return null;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }


    }
}
