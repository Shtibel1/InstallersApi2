using DAL.Abstracts;
using DAL.Data;
using DAL.Entities;
using DAL.Enums;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly CentralDbContext _context;

        public EmployeesRepository(CentralDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            employee.Id = Guid.NewGuid();

            var result = await _context.Employees.AddAsync(employee);
            if (result.Entity != null)
            {
                await _context.SaveChangesAsync();
                return result.Entity;
            }

            throw new Exception("problem saving entity");

        }

        public Task<List<Employee>> GetEmployeesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> GetEmployeeByUserId(Guid id)
        {
            return await _context.Employees.Include(e => e.Companies).FirstOrDefaultAsync(emp => emp.IdentityId == id);
        }


    }
}
