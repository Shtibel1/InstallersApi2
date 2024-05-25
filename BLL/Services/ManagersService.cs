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
        private readonly IEmployeesRepository _managersRepository;
        private readonly IMapper _mapper;

        public ManagersService(IEmployeesRepository managersRepository, IMapper mapper)
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

            var entity = _mapper.Map<Employee>(manager);
            
            var newManEntity = await _managersRepository.CreateEmployeeAsync(entity);
            var newMan = _mapper.Map<ManagerVm>(newManEntity);
            return newMan;
        }

        public Task<List<ManagerVm>> GetManagersAsync()
        {
            throw new NotImplementedException();
        }


    }
}
