using DAL.Data;
using DAL.Entities;
using DAL.Enums;
using DAL.Providers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Assignments
{
    public class AssignmentsRepository : IAssignmentsRepository
    {
        private readonly ICompanyDataProvider _companyDataProvider;

        public AssignmentsRepository(ICompanyDataProvider companyDataProvider)
        {
            _companyDataProvider = companyDataProvider;

        }

        public async Task<Assignment> GetAssignmentAsync(Guid id, CompanyNames company)
        {
            var context = _companyDataProvider.GetContext(company);

            Assignment? assignment = null;

            context.Assignments
                .Include(a => a.Product)
                .ThenInclude(p => p.Category)
                .Include(a => a.Customer)
                .Include(a => a.Comments)
                .FirstOrDefault(a => a.Id == id);

            return assignment;

        }

        public async Task<List<Assignment>> GetAssignmentsAsync(AssignmentsFilters? filters)
        {
            var contexts = _companyDataProvider.GetContexts();
            var tasks = new List<Task<List<Assignment>>>();

            foreach (var context in contexts)
            {
                var query = context.Assignments
                .Include(a => a.Product)
                .ThenInclude(p => p.Category)
                .Include(a => a.Customer)
                .Include(a => a.Comments)
                .AsQueryable();

                query = AssignmentsUtils.ApplyFilters(query, filters);
                tasks.Add(query.ToListAsync());
            }

            var assignments = await Task.WhenAll(tasks);
            return assignments.SelectMany(a => a).ToList();
        }

        public async Task<Assignment> CreateAssignmentAsync(Assignment assignment)
        {
            var context = _companyDataProvider.GetContext(assignment.companyName);  

            var savedAssignment = await context.Assignments.AddAsync(assignment).ConfigureAwait(false);
            await context.SaveChangesAsync();

            var newAssignment = await context.Assignments
                .Include(a => a.Product)
                .Include(a => a.Customer)
                .Include(a => a.Comments)
                .FirstOrDefaultAsync(a => a.Id == savedAssignment.Entity.Id);

            return newAssignment;
        }
        public async Task<Assignment> UpdateAssignmentAsync(Guid id, Assignment assignment)
        {
            var context = _companyDataProvider.GetContext(assignment.companyName);

            assignment.Id = id;
            context.Assignments.Update(assignment);
            await context.SaveChangesAsync();

            return await GetAssignmentAsync(id, assignment.companyName);
        }

        public async Task DeleteAssignmentAsync(Guid id, CompanyNames company)
        {
            var context = _companyDataProvider.GetContext(company);

            Assignment assignment = new Assignment() { Id = id };
            context.Assignments.Attach(assignment);
            var result = context.Assignments.Remove(assignment);
            await context.SaveChangesAsync();

        }

        public async Task PatchAssignmentAsync(Guid id, JsonPatchDocument assignment, CompanyNames company)
        {
            var context = _companyDataProvider.GetContext(company);
            var exist = await context.Assignments.FindAsync(id);
            assignment.ApplyTo(exist);
            await context.SaveChangesAsync();
        }

    }

    public class AssignmentsFilters
    {
        public PickupStatus? PickupStatus { get; set; }
        public AssignmentStatus? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? ManagerId { get; set; }
        public Guid? ServiceProviderId { get; set; }

    }
}
