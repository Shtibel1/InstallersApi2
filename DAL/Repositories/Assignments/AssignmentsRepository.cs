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


            var assignments = await context.Assignments
                .Include(a => a.Product)
                .ThenInclude(p => p.Category)
                .Include(a => a.Customer)
                .Include(a => a.Comments)
                .Include(a => a.Marketer)
                .Include(a => a.AssignmentAdditionalPrices)
                .ThenInclude(aap => aap.AdditionalPrice)
                .ThenInclude(ap => ap.Additional)
                .FirstOrDefaultAsync(a => a.Id == id);

            return assignments;

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
                .Include(a => a.AssignmentAdditionalPrices)
                .ThenInclude(aap => aap.AdditionalPrice)
                .ThenInclude(ap => ap.Additional)
                .AsQueryable();

                query = AssignmentsUtils.ApplyFilters(query, filters);
                tasks.Add(query.ToListAsync());
            }

            var assignments = await Task.WhenAll(tasks);
            return assignments.SelectMany(a => a).ToList();
        }

        public async Task<Guid> CreateAssignmentAsync(Assignment assignment)
        {
            var context = _companyDataProvider.GetContext(assignment.CompanyName);  
            assignment.PickupStatus = PickupStatus.NotReady;

            var savedAssignment = await context.Assignments.AddAsync(assignment).ConfigureAwait(false);
            await context.SaveChangesAsync();

            var newAssignment = await context.Assignments
                .Include(a => a.Product)
                .Include(a => a.Customer)
                .Include(a => a.Comments)
                .FirstOrDefaultAsync(a => a.Id == savedAssignment.Entity.Id);

            return newAssignment.Id;
        }
        public async Task UpdateAssignmentAsync(Guid id, Assignment assignment)
        {
            var context = _companyDataProvider.GetContext(assignment.CompanyName);

            //context.Entry(assignment).State = EntityState.Detached;
            //context.Assignments.Update(assignment);

            /*assignment.AssignmentAdditionalPrices.ForEach(aap =>
            {
                context.AssignmentAdditionalPrices.Add(aap);

            });*/


            await context.SaveChangesAsync();

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
