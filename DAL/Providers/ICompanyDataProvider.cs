using DAL.Data;
using DAL.Entities;
using DAL.Enums;

namespace DAL.Providers
{
    public interface ICompanyDataProvider
    {
        List<string>? GetConnectionStrings();
        void SetCompanies(List<CompanyNames> companies);
        List<CompanyNames>? GetCompanies();
        List<CompanyDbContext> GetContexts();
        CompanyDbContext GetContext(CompanyNames company);
    }
}
