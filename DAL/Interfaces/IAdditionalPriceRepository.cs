using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAdditionalPriceRepository
    {
        Task<List<AdditionalPrice>> Get(Guid SPId, Guid productId);
        Task<List<AdditionalPrice>> GetBySP(Guid SPId);
        Task<List<AdditionalPrice>> CreateAsync(List<AdditionalPrice> additionals);
        Task<List<AdditionalPrice>> UpdateAsync(List<AdditionalPrice> additional);
        Task DeleteAsync(Guid id);
    }
}
