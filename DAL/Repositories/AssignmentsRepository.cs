using DAL.Data;
using DAL.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AssignmentsRepository : IAssignmentsRepository
    {
        private readonly DataContext _context;

        public AssignmentsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Assignment> GetAssignmentAsync(int id)
        {
            var assignment = await _context.Assignments
                .Include(a => a.Manager)
                .Include(a => a.Installer)
                .ThenInclude(i => i.CategoryInstallers)
                .ThenInclude(ci => ci.Category)
                .Include(a => a.Product)
                .ThenInclude(p => p.Category)
                .Include(a => a.Customer)
                .Include(a => a.Comments)
                .FirstOrDefaultAsync(a => a.Id == id)
                .ConfigureAwait(false);

            return assignment;

        }

        public async Task<List<Assignment>> GetAssignmentsAsync(string id, string role)
        {
            if (role.Equals("manager"))
            {
                var assignments = await _context.Assignments
                   .Include(a => a.Manager)
                   .Include(a => a.Installer)
                   .ThenInclude(i => i.CategoryInstallers)
                   .ThenInclude(ci => ci.Category)
                   .Include(a => a.Product)
                   .ThenInclude(p => p.Category)
                   .Include(a => a.Customer)
                   .Include(a => a.Comments)
                   .ToListAsync();
                return assignments;

            }

            else
            {
                var assignments = await _context.Assignments
                    .Include(a => a.Manager)
                    .Include(a => a.Installer)
                    .Include(a => a.Product)
                    .Include(a => a.Customer)
                    .Include(a => a.Comments)
                    .Where(a => a.InstallerId == a.InstallerId)
                    .ToListAsync();
                return assignments;
            }
        }

        public async Task<Assignment> CreateAssignmentAsync(Assignment assignment)
        {
            var savedAssignment = await _context.Assignments.AddAsync(assignment).ConfigureAwait(false);
            await _context.SaveChangesAsync();

            var newAssignment = await _context.Assignments
                .Include(a => a.Manager)
                .Include(a => a.Installer)
                .Include(a => a.Product)
                .Include(a => a.Customer)
                .Include(a => a.Comments)
                .FirstOrDefaultAsync(a => a.Id == savedAssignment.Entity.Id);
            return newAssignment;
        }
        public async Task<Assignment> UpdateAssignmentAsync(int id, Assignment assignment)
        {

            assignment.Id = id;
            _context.Assignments.Update(assignment);
            await _context.SaveChangesAsync();

            return await GetAssignmentAsync(id);
        }

        public async Task DeleteAssignmentAsync(int id)
        {
            Assignment assignment = new Assignment() { Id = id };
            _context.Assignments.Attach(assignment);
            var result = _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();
            
        }

        public async Task PatchAssignmentAsync(int id, JsonPatchDocument assignment)
        {
            var exist = await _context.Assignments.FindAsync(id);
            assignment.ApplyTo(exist);
            await _context.SaveChangesAsync();
        }
    }
}
