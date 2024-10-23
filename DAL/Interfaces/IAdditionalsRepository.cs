using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAdditionalsRepository
    {
        Task<List<Additional>> GetAsync();
        Task<Additional> CreateAsync(Additional additional);
        Task<Additional?> UpdateAsync(Additional additional);
        Task<Additional> DeleteAsync(Guid id);
    }
}
