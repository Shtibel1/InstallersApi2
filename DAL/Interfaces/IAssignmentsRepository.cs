using DAL.Entities;
using DAL.Enums;
using DAL.Repositories.Assignments;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IAssignmentsRepository
    {
        Task<List<Assignment>> GetAssignmentsAsync(AssignmentsFilters? filters);
        Task<Assignment> GetAssignmentAsync(Guid id, CompanyNames company);
        Task<Guid> CreateAssignmentAsync(Assignment assignment);
        Task UpdateAssignmentAsync(Guid id, Assignment assignment);
        Task DeleteAssignmentAsync(Guid id, CompanyNames company);
        Task PatchAssignmentAsync(Guid id, JsonPatchDocument assignment, CompanyNames company);
    }
}
