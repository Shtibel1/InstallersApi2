using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMarketersService
    {
        Task<MarketerVm> CreateMarketerAsync(MarketerVm marketer);
        Task DeleteMarketerAsync(Guid id);
        Task<MarketerVm> GetMarketerAsync(Guid id);
        Task<List<MarketerVm>> GetMarketersAsync();
        Task<MarketerVm> UpdateMarketerAsync(MarketerVm marketer);
    }
}
