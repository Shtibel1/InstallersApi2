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
        [Authorize]
        public async Task<ActionResult<IEnumerable<AssignmentVm>>> GetAssignments()
        {
            
            try
            {
                string jwt = Request.Headers["Authorization"];
                string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                string role = User.FindFirstValue(ClaimTypes.Role);
                var assignmentsDto = await _assignmentService.GetAssignmentsAsync(id, role);


                return Ok(assignmentsDto);

            }
            catch (Exception ex)
            {

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
        public async Task<IActionResult> PatchAssignment(int id, [FromBody] JsonPatchDocument assignment)
        {
            try
            {
                //if the user is an installer and the assignment is not a status update
                if (User.IsInRole(Roles.Installer) && !(assignment.Operations != null && assignment.Operations.Count == 1 && assignment.Operations[0].path.Equals("/status")))
                {
                    return StatusCode(StatusCodes.Status403Forbidden);
                }

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
