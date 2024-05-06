using BLL.Models;
using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAssignmentsService
    {
        Task<List<AssignmentVm>> GetAssignmentsAsync(string id, string role);
        Task<AssignmentVm> GetAssignmentAsync(int id);
        Task<AssignmentVm> CreateAssignmentAsync(CreateAssignmentVm assignment);
        Task<AssignmentVm> UpdateAssignmentAsync(int id, CreateAssignmentVm assignment);
        Task DeleteAssignmentAsync(int id);
        Task PatchAssignmentAsync(int id, JsonPatchDocument assignment);


    }
}
