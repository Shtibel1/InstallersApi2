using DAL.Entities;
using DAL.Interfaces;
using DAL.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CalaulationsRepository : ICalaulationsRepository
    {
        private readonly ICompanyDataProvider _companyDataProvider;

        public CalaulationsRepository(ICompanyDataProvider companyDataProvider)
        {
            _companyDataProvider = companyDataProvider;
        }

        public async Task Create(Calculation calaulationVm)
        {
            var context = _companyDataProvider.GetContexts()[0];
            await context.Calculation.AddAsync(calaulationVm);
            await context.SaveChangesAsync();
        }

        public async Task<Calculation> Get(Guid id)
        {
            var context = _companyDataProvider.GetContexts()[0];
            var result = await context.Calculation.Include(c => c.CalculationAssignments).ThenInclude(ca => ca.Assignment).FirstOrDefaultAsync(c => c.Id == id);
            return result;
        }

        public async Task<List<Calculation>> Get()
        {
            var context = _companyDataProvider.GetContexts()[0];
            return await context.Calculation.ToListAsync();
        }
    }

}
