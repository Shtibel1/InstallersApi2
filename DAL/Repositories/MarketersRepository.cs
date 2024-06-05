using DAL.Entities;
using DAL.Interfaces;
using DAL.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class MarketersRepository : IMarketersRepository
    {
        private readonly ICompanyDataProvider _companyDataProvider;

        public MarketersRepository(ICompanyDataProvider companyDataProvider)
        {
            _companyDataProvider = companyDataProvider;
        }

        public async Task<Marketer> CreateMarketerAsync(Marketer marketer)
        {
            var context = _companyDataProvider.GetContexts()[0];
            var result = await context.Marketers.AddAsync(marketer);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteMarketerAsync(Guid id)
        {
            var context = _companyDataProvider.GetContexts()[0];
            var entity = await context.Marketers.FirstOrDefaultAsync(m => m.Id == id);
            if (entity != null)
            {
                context.Marketers.Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Marketer> GetMarketerAsync(Guid id)
        {
            var context = _companyDataProvider.GetContexts()[0];
            return await context.Marketers.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Marketer>> GetMarketersAsync()
        {
            var context = _companyDataProvider.GetContexts()[0];
            return await context.Marketers.ToListAsync();
        }

        public async Task<Marketer> UpdateMarketerAsync(Marketer marketer)
        {
            var context = _companyDataProvider.GetContexts()[0];
            var entity = await context.Marketers.FirstOrDefaultAsync(m => m.Id == marketer.Id);
            if (entity != null)
            {
                context.Entry(entity).CurrentValues.SetValues(marketer);
                await context.SaveChangesAsync();
                return entity;
            }
            return null;
        }
    }
}
