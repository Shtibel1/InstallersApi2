using BLL.Vms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICalaulationsService
    {
        Task Create(CalaulationVm calaulationVm);
        Task<CalaulationVm> Get(Guid id);
        Task<List<CalaulationVm>> Get();
    }
}
