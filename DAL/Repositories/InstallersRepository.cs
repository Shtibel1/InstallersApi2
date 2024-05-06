using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class InstallersRepository : IInstallersRepository
    {
        private readonly DataContext _context;

        public InstallersRepository(DataContext context)
        {

            _context = context;
        }

        public async Task<List<Installer>> GetInstallersAsync()
        {
            var installers = await _context.Installers.Include(i => i.CategoryInstallers).ThenInclude(ct => ct.Category).ToListAsync();
            
            return installers;
        }

        public async Task<Installer> CreateInstallerAsync(Installer installer, List<int> categoriesIds)
        {
            installer.Id = Guid.NewGuid();

            var categories = await _context.Categories.Where(c => categoriesIds.Contains(c.Id)).ToListAsync();
            installer.CategoryInstallers = categories.Select(c => new CategoryInstaller 
            { 
                CategoryId = c.Id, InstallerId = installer.Id 
            }).ToList();

            await _context.Installers.AddAsync(installer);
            await _context.SaveChangesAsync();
            return installer;

        }

        public async Task<List<Installer>> GetInstallerAsync(Guid id)
        {
            var installer = await _context.Installers.Include(i => i.CategoryInstallers).ThenInclude(ci => ci.Category).FirstOrDefaultAsync(i => i.Id == id);
            return new List<Installer> { installer };
        }

        public async Task<Installer> GetInstallerbyUserId(string id)
        {
            return await _context.Installers.FirstOrDefaultAsync(manager => manager.IdentityId == id);
        }
    }
}
