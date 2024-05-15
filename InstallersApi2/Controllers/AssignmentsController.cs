using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Models;
using BLL.Interfaces;
using System.Security.Claims;
using DAL.Enums;
using Microsoft.AspNetCore.JsonPatch;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BLL.Services;

namespace InstallersApi2.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentsService _assignmentService;

        public AssignmentsController(IAssignmentsService assignmentService)
        {
            _assignmentService = assignmentService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssignmentVm>>> GetAssignments()
        {
            return await GetAssignmentsInternal(User.FindFirstValue(ClaimTypes.NameIdentifier), User.FindFirstValue(ClaimTypes.Role));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<IEnumerable<AssignmentVm>>> GetAssignmentsByInstaller(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Invalid installer ID");
            }

            return await GetAssignmentsInternal(id, User.FindFirstValue(ClaimTypes.Role));
        }

        private async Task<ActionResult<IEnumerable<AssignmentVm>>> GetAssignmentsInternal(string userId, string role)
        {
            try
            {
                var assignmentsDto = await _assignmentService.GetAssignmentsAsync(userId, role);
                return Ok(assignmentsDto);
            }
            catch (Exception ex)
            {
                // Consider logging the exception details here
                return BadRequest("FAILED_GET_ASSIGNMENTS");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssignmentVm>> GetAssignment(int id)
        {
            try
            {
                var assignment = await _assignmentService.GetAssignmentAsync(id);

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
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> PutAssignment(int id, CreateAssignmentVm assignment)
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

        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> PatchAssignment(int id, [FromBody] JsonPatchDocument assignment)
        {
            try
            {
                await _assignmentService.PatchAssignmentAsync(id, assignment);

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
        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            try
            {
                await _assignmentService.DeleteAssignmentAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest("FAILED_DELETE_ASSIGNMENT");
            }
        }


    }
}
