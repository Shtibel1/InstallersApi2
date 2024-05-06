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
    public class ManagersRepository : IManagersRepository
    {
        private readonly DataContext _context;

        public ManagersRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Manager> CreateManagerAsync(Manager manager)
        {
            manager.Id = Guid.NewGuid();
            var result = await _context.Managers.AddAsync(manager);
            if (result.Entity != null)
            {
                await _context.SaveChangesAsync();
                return result.Entity;
            }

            throw new Exception("problem saving entity");

        }

        public Task<List<Manager>> GetManagersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Manager> GetManagerbyUserId(string id)
        {
            return await _context.Managers.FirstOrDefaultAsync(manager => manager.IdentityId == id);
        }
    }
}
