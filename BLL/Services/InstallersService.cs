using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class InstallersService : IInstallersService
    {
        private readonly IInstallersRepository _accountRepository;
        private readonly IMapper _mapper;

        public InstallersService(IInstallersRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<InstallerDto> CreateInstallerAsync(CreateInstallerModel installer)
        {
            var entity = _mapper.Map<Installer>(installer);
            var newIns = _mapper.Map<InstallerDto>(await _accountRepository.CreateInstallerAsync(entity, installer.Categories.ToList()));
            return newIns;
        }

        public async Task<List<InstallerDto>> GetInstallersAsync()
        {
            var entities = await _accountRepository.GetInstallersAsync();
            var installers = new List<InstallerDto>();
            var categories = new List<CategoryDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                foreach (var ci in entities[i].CategoryInstallers.ToList())
                {
                    categories.Add(new CategoryDto
                    {
                        Id = ci.CategoryId,
                        Name = ci.Category.Name
                    });
                }

                installers.Add(new InstallerDto
                {
                    Id = entities[i].Id,
                    Name = entities[i].Name,
                    Role = entities[i].Role,
                    Phone = entities[i].Phone,
                    Categories = categories
                });

                categories = new List<CategoryDto>();

            }

            return installers;
        }

        public async Task<List<InstallerDto>> GetInstallerAsync(Guid id)
        {
            var entity = await _accountRepository.GetInstallerAsync(id);
            return _mapper.Map<List<InstallerDto>>(entity);
        }
    }
}
