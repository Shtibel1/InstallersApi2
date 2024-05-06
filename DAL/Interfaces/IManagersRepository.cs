using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IManagersRepository
    {
        Task<List<Manager>> GetManagersAsync();
        Task<Manager> CreateManagerAsync(Manager manager);
        Task<Manager> GetManagerbyUserId(string id);
    }
}
