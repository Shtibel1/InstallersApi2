using BLL.DTOs;
using BLL.Vms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAdditionalPriceService
    {
        Task<AdditionalPriceVm> Get(Guid SPId, Guid productId);
        Task<List<AdditionalPriceVm>> GetBySP(Guid SPId);
        Task<List<AdditionalPriceVm>> CreateAsync(List<AdditionalPriceVm> additionals);
        Task<List<AdditionalPriceVm>> UpdateAsync(List<AdditionalPriceVm> additional);
        Task DeleteAsync(Guid id);
    }
}
