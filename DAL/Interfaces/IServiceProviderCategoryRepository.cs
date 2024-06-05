using DAL.Entities;

namespace InstallersApi2.Controllers
{
    public interface IServiceProviderCategoryRepository
    {
        Task<bool> AddCategoriesToServiceProviderAsync(List<Category> categories, Guid serviceProviderId);
    }
}