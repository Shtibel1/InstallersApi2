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
    public class AdditionalsRepository : IAdditionalsRepository
    {
        private readonly ICompanyDataProvider _companyDataProvider;

        public AdditionalsRepository(ICompanyDataProvider companyDataProvider)
        {
            _companyDataProvider = companyDataProvider;
        }
        public async Task<List<Additional>> GetAsync()
        {
            var context = _companyDataProvider.GetContexts()[0];

            return await context.Additionals.ToListAsync();
        }

        public async Task<Additional?> UpdateAsync(Additional additional)
        {
            var context = _companyDataProvider.GetContexts()[0];
            context.Additionals.Update(additional);
           
            await context.SaveChangesAsync();
            return await context.Additionals.FirstOrDefaultAsync(a => a.Id == additional.Id);
        }
        public async Task<Additional> CreateAsync(Additional additional)
        {
            var context = _companyDataProvider.GetContexts()[0];
            var result = await context.Additionals.AddAsync(additional).ConfigureAwait(false);
            await context.SaveChangesAsync();
            var newAdd = await context.Additionals.FirstOrDefaultAsync(a => a.Id == additional.Id);
            return newAdd;
        }

        public async Task<Additional> DeleteAsync(Guid id)
        {
            var context = _companyDataProvider.GetContexts()[0];
            var additional = await context.Additionals.FindAsync(id);

            if (additional == null)
            {
                throw new Exception("Additional not found");
            }

            context.Additionals.Remove(additional);
            await context.SaveChangesAsync();
            return additional;
        }

    }
}
