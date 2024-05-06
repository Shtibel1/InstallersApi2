using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace BLL.Services
{
    public class ManagersService : IManagersService
    {
        private readonly IManagersRepository _managersRepository;
        private readonly IMapper _mapper;

        public ManagersService(IManagersRepository managersRepository, IMapper mapper)
        {
            _managersRepository = managersRepository;
            _mapper = mapper;
        }

        public async Task<ManagerVm> CreateManagerAsync(ManagerVm manager)
        {
            /*var entity = new Manager
            {
                Id = manager.Id,
                Name = manager.Name,
                Phone = manager.Phone,
                Role = manager.Role
            };*/

            var entity = _mapper.Map<Manager>(manager);
            
            var newManEntity = await _managersRepository.CreateManagerAsync(entity);
            var newMan = _mapper.Map<ManagerVm>(newManEntity);
            return newMan;
        }

        public Task<List<ManagerVm>> GetManagersAsync()
        {
            throw new NotImplementedException();
        }


    }
}
