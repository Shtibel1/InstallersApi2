using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class InstallerPricingRepository : IInstallerPricingRepository
    {
        private readonly DataContext _context;

        public InstallerPricingRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<List<InstallerPricing>> GetInstallerPricingByInstallerAsync(Guid installerId)
        {
            return await _context.InstallerPricing
                .Where(p => p.InstallerId == installerId)
                .ToListAsync();
        }

        public async Task<InstallerPricing> UpdateInstallerPricingAsync(Guid installerId, InstallerPricing InstallerPricing)
        {
            InstallerPricing.InstallerId = installerId;
            var updatedPrice = await _context.InstallerPricing.AddAsync(InstallerPricing);
            await _context.SaveChangesAsync();
            return updatedPrice.Entity;
        }

        public async Task DeleteInstallerPricingAsync(Guid installerId)
        {
            var installerInstallerPricing = await _context.InstallerPricing.Where(p => p.InstallerId == installerId).ToListAsync();
            if (installerInstallerPricing != null)
            {
                for (int i = 0; i < installerInstallerPricing.Count; i++)
                {
                    _context.Remove(installerInstallerPricing[i]);
                }
            
                await _context.SaveChangesAsync();

            }
        }


        public async Task<InstallerPricing> GetInstallerPicing(Guid installerId, int productId)
        {
            return await _context.InstallerPricing.FirstOrDefaultAsync(i => i.ProductId == productId && i.InstallerId == installerId);
        }



        /*public async Task<InstallerPricing> CreateInstallerPricingAsync(InstallerPricing InstallerPricing)
        {
            var newInstallerPricing = await _context.AddAsync(InstallerPricing).ConfigureAwait(false);
            await _context.SaveChangesAsync();
            return newInstallerPricing.Entity;
        }*/


    }
}
