using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<List<Employee>> GetEmployeesAsync();
        Task<Employee> CreateEmployeeAsync(Employee manager);
        Task<Employee> GetEmployeeByUserId(Guid id);
    }
}
