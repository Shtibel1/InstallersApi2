using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IManagersService
    {
        Task<List<ManagerVm>> GetManagersAsync();
        Task<ManagerVm> CreateManagerAsync(ManagerVm manager);
    }
}
