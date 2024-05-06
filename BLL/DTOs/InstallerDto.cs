using BLL.DTOs.Abstracts;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class InstallerDto : WorkerDto
    {
        public List<CategoryDto> Categories { get; set; }

    }

 
}
