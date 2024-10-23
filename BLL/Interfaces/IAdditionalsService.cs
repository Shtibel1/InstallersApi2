using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAdditionalsService
    {
        Task<List<AdditionalVm>> GetAsync();
        Task<AdditionalVm> CreateAsync(AdditionalVm additional);
        Task<AdditionalVm> UpdateAsync(AdditionalVm additional);
        Task DeleteAsync(Guid id);
    }
}
