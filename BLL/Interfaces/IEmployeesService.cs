using BLL.Models;
using InstallersApi2.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace InstallersApi2.Controllers
{
    public interface IEmployeesService
    {
        Task<ServiceProviderVm> AddCategoriesToServiceProviderAsync(List<CategoryVm> categories, Guid serviceProviderId);

        
    }
}
