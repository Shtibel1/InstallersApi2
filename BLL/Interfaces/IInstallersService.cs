using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IInstallersService
    {
        Task<List<InstallerDto>> GetInstallersAsync();
        Task<List<InstallerDto>> GetInstallerAsync(Guid id);
        Task<InstallerDto> CreateInstallerAsync(CreateInstallerModel installer);
    }
}
