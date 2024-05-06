using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IInstallersRepository
    {
        Task<List<Installer>> GetInstallersAsync();
        Task<List<Installer>> GetInstallerAsync(Guid id);
        Task<Installer> CreateInstallerAsync(Installer installer, List<int> categories);
        Task<Installer> GetInstallerbyUserId(string id);
    }
}
