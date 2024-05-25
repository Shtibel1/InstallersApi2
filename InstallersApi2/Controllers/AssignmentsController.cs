using BLL.Interfaces;
using BLL.Models;
using DAL.Enums;
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

        public AssignmentsController(IAssignmentsService assignmentService)
        {
            _assignmentService = assignmentService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssignmentVm>>> GetAssignments(AssignmentsFilters? filters)
        {
            return await GetAssignmentsInternal(User.FindFirstValue(ClaimTypes.NameIdentifier), User.FindFirstValue(ClaimTypes.Role), filters);
        }

        [HttpPost("filter")]
        
        public async Task<ActionResult<IEnumerable<AssignmentVm>>> GetAssignmentsByInstaller(filtersVm filters)
        {   
            if (string.IsNullOrWhiteSpace(filters.InstallerId))
            {
                return BadRequest("Invalid installer ID");
            }

            var user = User.FindAll(ClaimTypes.NameIdentifier);

            return await GetAssignmentsInternal(filters.InstallerId, User.FindFirstValue(ClaimTypes.Role), null);
        }

        private async Task<ActionResult<IEnumerable<AssignmentVm>>> GetAssignmentsInternal(string userId, string role, AssignmentsFilters? filters)
        {
            try
            {
                var assignmentsDto = await _assignmentService.GetAssignmentsAsync(userId, role, filters);
                return Ok(assignmentsDto);
            }
            catch (Exception ex)
            {
                // Consider logging the exception details here
                return BadRequest("FAILED_GET_ASSIGNMENTS");
            }
        }

        [HttpGet("{companyName}/{id}")]
        public async Task<ActionResult<AssignmentVm>> GetAssignment(CompanyNames companyName, Guid id)
        {
            try
            {
                var assignment = await _assignmentService.GetAssignmentAsync(id, companyName);

                if (assignment == null)
                {
                    return Ok();
                }

                return Ok(assignment);
            }
            catch (Exception)
            {

                return BadRequest("FAILED_GET_ASSIGNMENT");
            }
            
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(Role.Employee))]
        public async Task<IActionResult> PutAssignment(Guid id, CreateAssignmentVm assignment)
        {
            try
            {
                var updatedAssignment = await _assignmentService.UpdateAssignmentAsync(id, assignment);
                return Ok(updatedAssignment);
            }
            catch (Exception ex)
            {
                return BadRequest("FAILED_UPDATE_ASSIGNMENT");
            }
        }

        [HttpPatch("{companyName}/{id}")]
        [Authorize]
        public async Task<IActionResult> PatchAssignment(CompanyNames companyName, Guid id, [FromBody] JsonPatchDocument assignment)
        {
            try
            {
                await _assignmentService.PatchAssignmentAsync(id, assignment, companyName);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest("FAILED_UPDATE_ASSIGNMENT");
            }
        }

        [HttpPost]
        // [Authorize(Roles = Roles.Manager)]
        public async Task<ActionResult<AssignmentVm>> PostAssignment(CreateAssignmentVm assignment)
        {
            try
            {
                
                var newAssignment = await _assignmentService.CreateAssignmentAsync(assignment);
                return Ok(newAssignment);
            }
            catch (Exception ex)
            {

                return BadRequest("FAILED_CREATE_ASSIGNMENT");
            }
        }

        // DELETE: api/Assignments/5
        [HttpDelete("{companyName}/{id}")]
        [Authorize(Roles = nameof(Role.Employee))]
        public async Task<IActionResult> DeleteAssignment(CompanyNames companyName, Guid id)
        {
            try
            {
                await _assignmentService.DeleteAssignmentAsync(id, companyName);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest("FAILED_DELETE_ASSIGNMENT");
            }
        }


    }

    public class filtersVm
    {
        public string InstallerId { get; set; }
    }


}
