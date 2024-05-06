using DAL.Entities;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IAssignmentsRepository
    {
        Task<List<Assignment>> GetAssignmentsAsync(string id, string role);
        Task<Assignment> GetAssignmentAsync(int id);
        Task<Assignment> CreateAssignmentAsync(Assignment assignment);
        Task<Assignment> UpdateAssignmentAsync(int id, Assignment assignment);
        Task DeleteAssignmentAsync(int id);
        Task PatchAssignmentAsync(int id, JsonPatchDocument assignment);
    }
}
