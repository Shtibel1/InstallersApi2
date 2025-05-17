using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICalaulationsRepository 
    {
        Task Create(Calculation calaulationVm);
        Task<Calculation> Get(Guid id);
        Task<List<Calculation>> Get();
    }
}
