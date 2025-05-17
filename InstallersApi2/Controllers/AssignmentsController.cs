using BLL.Interfaces;
using BLL.Models;
using DAL.Enums;
using DAL.Providers;
using DAL.Repositories.Assignments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InstallersApi2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentsService _assignmentService;
        private readonly ICompanyDataProvider _companyDataProvider;

        public AssignmentsController(IAssignmentsService assignmentService, ICompanyDataProvider companyDataProvider)
        {
            _assignmentService = assignmentService;
            _companyDataProvider = companyDataProvider;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssignmentVm>>> GetAssignments([FromQuery]AssignmentsFilters? filters)
        {

            return await GetAssignmentsInternal(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), User.FindFirstValue(ClaimTypes.Role), filters);
        }

        [HttpPost("filter")]

        public async Task<ActionResult<IEnumerable<AssignmentVm>>> GetAssignmentsByInstaller(filtersVm filters)
        {
            if (filters.InstallerId == null)
            {
                return BadRequest("Invalid installer ID");
            }

            var user = User.FindAll(ClaimTypes.NameIdentifier);

            return await GetAssignmentsInternal(filters.InstallerId, User.FindFirstValue(ClaimTypes.Role), new AssignmentsFilters() { ServiceProviderId = filters.InstallerId});
        }

        private async Task<ActionResult<IEnumerable<AssignmentVm>>> GetAssignmentsInternal(Guid userId, string role, AssignmentsFilters? filters)
        {

            var assignmentsDto = await _assignmentService.GetAssignmentsAsync(userId, role, filters);
            return Ok(assignmentsDto);
        }

        [HttpGet("{CompanyName}/{id}")]
        public async Task<ActionResult<AssignmentVm>> GetAssignment(CompanyNames companyName, Guid id)
        {

            var assignment = await _assignmentService.GetAssignmentAsync(id, companyName);

            if (assignment == null)
            {
                return Ok();
            }

            return Ok(assignment);

        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(Role.Employee))]
        public async Task<IActionResult> PutAssignment(Guid id, CreateAssignmentVm assignment)
        {
            if (_companyDataProvider?.GetCompanies()?[0] == null)
            {
                return BadRequest("INVALID_COMPANY_NAME");
            }


            var updatedAssignment = await _assignmentService.UpdateAssignmentAsync(id, assignment, _companyDataProvider.GetCompanies()[0]);
            return Ok(updatedAssignment);

        }

        [HttpPatch("{CompanyName}/{id}")]
        [Authorize]
        public async Task<IActionResult> PatchAssignment(CompanyNames companyName, Guid id, [FromBody] JsonPatchDocument assignment)
        {

            await _assignmentService.PatchAssignmentAsync(id, assignment, companyName);

            return NoContent();

        }

        [HttpPost]
        [Authorize(Roles = nameof(Role.Employee))]
        public async Task<ActionResult<AssignmentVm>> PostAssignment(CreateAssignmentVm assignment)
        {

            var newAssignment = await _assignmentService.CreateAssignmentAsync(assignment, _companyDataProvider.GetCompanies()[0]);
            return Ok(newAssignment);

        }

        // DELETE: api/Assignments/5
        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(Role.Employee))]
        public async Task<IActionResult> DeleteAssignment(Guid id)
        {

            await _assignmentService.DeleteAssignmentAsync(id, _companyDataProvider.GetCompanies()[0]);
            return NoContent();

        }

        [HttpGet("script")]
        public async Task<IActionResult> GetScript()
        {
            using var client = new HttpClient();

            var response = await client.GetAsync("https://copilot.microsoft.com/webchat/bootstrapper.js");

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            var script = await response.Content.ReadAsStringAsync();

            return Content(script, "application/javascript");
        }


    }

    public class filtersVm
    {
        public Guid InstallerId { get; set; }
    }


}
