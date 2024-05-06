using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.Abstracts;

namespace BLL.Models
{
    public class InstallerCreateInstallerModel : WorkerDto
    {
        public List<int> Categories { get; set; }
    }
}
