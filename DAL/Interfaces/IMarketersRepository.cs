using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IMarketersRepository
    {
        Task<List<Marketer>> GetMarketersAsync();
        Task<Marketer> GetMarketerAsync(Guid id);
        Task<Marketer> CreateMarketerAsync(Marketer marketer);
        Task<Marketer> UpdateMarketerAsync(Marketer marketer);
        Task DeleteMarketerAsync(Guid id);

    }
}
