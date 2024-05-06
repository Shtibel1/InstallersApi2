using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AssignmentsService : IAssignmentsService
    {
        private readonly IAssignmentsRepository _assignmentsRepository;
        private readonly IMapper _mapper;
        private readonly IProductsRepository _productsService;

        public AssignmentsService(
            IAssignmentsRepository assignmentsRepository,
            IMapper mapper)
        {
            _assignmentsRepository = assignmentsRepository;
            _mapper = mapper;
        }
        public async Task<AssignmentVm> GetAssignmentAsync(int id)
        {
            var assigment = await _assignmentsRepository.GetAssignmentAsync(id);
            var assignmentVm = _mapper.Map<AssignmentVm>(assigment);

            //var product = await _productsService.GetProductsByCategoryId(assigment.Product.CategoryId);


            return assignmentVm;
        }

        public async Task<List<AssignmentVm>> GetAssignmentsAsync(string id, string role)
        {
            var assignments = _mapper.Map<List<AssignmentVm>>(await _assignmentsRepository.GetAssignmentsAsync(id, role));
            return assignments;
        }

        public async Task<AssignmentVm> CreateAssignmentAsync(CreateAssignmentVm assignment)
        {
            var entity = _mapper.Map<Assignment>(assignment);
            var a = JsonConvert.SerializeObject(entity);

            if (assignment.Comments != null)
            {
                entity.Comments = new List<Comment>
                {
                    new Comment
                    {
                        Id = 0,
                        Content = assignment.Comments[0].Content,
                        WorkerId = assignment.ManagerId,
                    }
                };
                
            };

            var createdAssignment = (await _assignmentsRepository.CreateAssignmentAsync(entity));
            var newAs = _mapper.Map<AssignmentVm>(createdAssignment);
            return newAs;
        }
        public async Task<AssignmentVm> UpdateAssignmentAsync(int id, CreateAssignmentVm assignment)
        {
            var entity = _mapper.Map<Assignment>(assignment);
            var updatedAs = await _assignmentsRepository.UpdateAssignmentAsync(id, entity);
            return _mapper.Map<AssignmentVm>(updatedAs);
        }

        public async Task PatchAssignmentAsync(int id, JsonPatchDocument assignment)
        {
            await _assignmentsRepository.PatchAssignmentAsync(id, assignment);
        }

        public async Task DeleteAssignmentAsync(int id)
        {
             await _assignmentsRepository.DeleteAssignmentAsync(id);
        }

    }

}
