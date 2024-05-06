using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IWorkersRepository
    {
        
        Task<Manager> CreateManagerAsync(Manager manager);

    }
}