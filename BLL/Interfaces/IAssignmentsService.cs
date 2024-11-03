using BLL.Models;
using BLL.Services;
using DAL.Entities;
using DAL.Enums;
using DAL.Repositories.Assignments;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAssignmentsService
    {
        Task<List<AssignmentVm>> GetAssignmentsAsync(Guid id, string role, AssignmentsFilters? filters);
        Task<AssignmentVm> GetAssignmentAsync(Guid id, CompanyNames companyNames);
        Task<AssignmentVm> CreateAssignmentAsync(CreateAssignmentVm assignment, CompanyNames companyNames);
        Task<AssignmentVm> UpdateAssignmentAsync(Guid id, CreateAssignmentVm assignment, CompanyNames companyName);
        Task DeleteAssignmentAsync(Guid id, CompanyNames companyName);
        Task PatchAssignmentAsync(Guid id, JsonPatchDocument assignment, CompanyNames companyName);


    }
}
