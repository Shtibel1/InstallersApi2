using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Vms;
using DAL.Entities;
using DAL.Enums;
using DAL.Interfaces;
using DAL.Repositories;
using DAL.Repositories.Assignments;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
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
        private readonly IServiceProvidersRepository _serviceProvidersRepository;
        private readonly IMapper _mapper;
        private readonly IProductsRepository _productsService;

        public AssignmentsService(
            IAssignmentsRepository assignmentsRepository,
            IServiceProvidersRepository serviceProvidersRepository,
            IMapper mapper)
        {
            _assignmentsRepository = assignmentsRepository;
            _serviceProvidersRepository = serviceProvidersRepository;
            _mapper = mapper;
        }
        public async Task<AssignmentVm> GetAssignmentAsync(Guid id, CompanyNames companyNames)
        {
            var assigment = await _assignmentsRepository.GetAssignmentAsync(id, companyNames);
            var serviceProvider = await _serviceProvidersRepository.GetserviceProviderAsync(assigment.ServiceProviderIdExt);
            var assignmentVm = _mapper.Map<AssignmentVm>(assigment);

            assignmentVm.AdditionalPrices = _mapper.Map<List<AdditionalPriceVm>>(assigment.AssignmentAdditionalPrices.Select(aap => aap.AdditionalPrice));

            assignmentVm.ServiceProvider = _mapper.Map<ServiceProviderVm>(serviceProvider);

            //var product = await _productsService.GetProductsByCategoryId(assigment.Product.CategoryId);


            return assignmentVm;
        }

        public async Task<List<AssignmentVm>> GetAssignmentsAsync(Guid id, string role, AssignmentsFilters? filters)
        {
            var assEntities = await _assignmentsRepository.GetAssignmentsAsync(filters);
            var serviceProvidersIds = assEntities.Select(a => a.ServiceProviderIdExt).Distinct().ToList();
            var serviceProviders = await _serviceProvidersRepository.GetServiceProvidersByIds(serviceProvidersIds);



            var assignmentsVms = _mapper.Map<List<AssignmentVm>>(assEntities);

            for (int i = 0; i < assignmentsVms.Count; i++)
            {
                var addtionalPrices = assEntities[i].AssignmentAdditionalPrices.Select(aap => aap.AdditionalPrice).ToList();
                var addtionalPricesVm = _mapper.Map<List<AdditionalPriceVm>>(addtionalPrices);
                assignmentsVms[i].AdditionalPrices = addtionalPricesVm;
            }
            
            var serviceProvidersVm = _mapper.Map<List<ServiceProviderVm>>(serviceProviders);

            assignmentsVms.ForEach(a => a.ServiceProvider = serviceProvidersVm.FirstOrDefault(sp => sp.Id == a.ServiceProvider.Id));

            return assignmentsVms;
        }

        public async Task<AssignmentVm> CreateAssignmentAsync(CreateAssignmentVm assignment, CompanyNames companyName)
        {
            assignment.CompanyName = companyName;


            var entity = _mapper.Map<Assignment>(assignment);

            assignment.AdditionalPrices.ForEach(price =>
            {
                entity.AssignmentAdditionalPrices.Add(new AssignmentAdditionalPrice
                {
                    Assignment = entity,
                    AdditionalPriceId = price.Id
                });
            });

            entity.ServiceProviderIdExt = assignment.ServiceProviderId;

            var createdAssignmentId = (await _assignmentsRepository.CreateAssignmentAsync(entity));
            var createdAssignment = await GetAssignmentAsync(createdAssignmentId, companyName);  

            var newAs = _mapper.Map<AssignmentVm>(createdAssignment);
            return newAs;
        }
        public async Task<AssignmentVm> UpdateAssignmentAsync(Guid id, CreateAssignmentVm assignment, CompanyNames companyNames)
        {
            assignment.CompanyName = companyNames;

            var existingAssignment = await _assignmentsRepository.GetAssignmentAsync(id, companyNames);
            if (existingAssignment == null)
            {
                throw new InvalidOperationException($"Assignment with id {id} not found.");
            }

            existingAssignment.CompanyName = companyNames;
            existingAssignment.CreatedDate = assignment.CreatedDate;
            existingAssignment.AssignmentDate = null;
            existingAssignment.CustomerNeedsToPay = assignment.CustomerNeedsToPay;
            existingAssignment.CustomerAlreadyPaid = null;
            existingAssignment.Cost = assignment.Cost;
            existingAssignment.Extras = assignment.Extras;
            existingAssignment.Price = null;
            existingAssignment.Status = assignment.Status;
            existingAssignment.ProductId = assignment.ProductId;
            existingAssignment.Customer = _mapper.Map<Customer>(assignment.Customer);
            existingAssignment.EmployeeId = assignment.EmployeeId;
            existingAssignment.ServiceProviderIdExt = assignment.ServiceProviderId;
            existingAssignment.MarketerId = assignment.MarketerId;
            existingAssignment.Comments = _mapper.Map<List<Comment>>( assignment.Comments);

            // Handle many-to-many relationship
            var additionalPrices = _mapper.Map<List<AdditionalPrice>>(assignment.AdditionalPrices);
            existingAssignment.AssignmentAdditionalPrices.Clear(); // Clear existing relationships

            foreach (var additionalPrice in additionalPrices)
            {
                existingAssignment.AssignmentAdditionalPrices.Add(new AssignmentAdditionalPrice
                {
                    AdditionalPriceId = additionalPrice.Id,
                    AssignmentId = existingAssignment.Id
                });
            }

            await _assignmentsRepository.UpdateAssignmentAsync(id, existingAssignment);
            var updatedAs = await GetAssignmentAsync(id, companyNames);
            return _mapper.Map<AssignmentVm>(updatedAs);
        }

        public async Task PatchAssignmentAsync(Guid id, JsonPatchDocument assignment, CompanyNames companyName)
        {
            await _assignmentsRepository.PatchAssignmentAsync(id, assignment, companyName);
        }

        public async Task DeleteAssignmentAsync(Guid id, CompanyNames companyName)
        {
             await _assignmentsRepository.DeleteAssignmentAsync(id, companyName);
        }

    }

}
