using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using BLL.Services.AuthService;
using DAL.Data;
using DAL.Entities;
using DAL.Enums;
using DAL.Providers;

namespace InstallersApi2.Middlewares
{
    public class CompanyMiddleware
    {
        private readonly RequestDelegate _next;

        public CompanyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            //var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            //var business = GetCompaniesFromToken(token);
            var business = new List<CompanyNames> { CompanyNames.Shtibay };
            var businessContext = serviceProvider.GetRequiredService<ICompanyDataProvider>();
            businessContext.SetCompanies(business);

            await _next(context);
        }

        private List<CompanyNames> GetCompaniesFromToken(string? token)
        {
            if (string.IsNullOrEmpty(token)) return null;

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var companies = jwtToken.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.Companies);

            if (companies == null || string.IsNullOrEmpty(companies.Value))
            {
                throw new UnauthorizedAccessException("No company information found in the token.");
            }

            List<string> companyNameStrings;
            try
            {
                companyNameStrings = JsonSerializer.Deserialize<List<string>>(companies.Value);
            }
            catch (JsonException)
            {
                throw new UnauthorizedAccessException("Invalid company information format in the token.");
            }

            var companyNames = new List<CompanyNames>();
            foreach (var companyNameString in companyNameStrings)
            {
                if (Enum.TryParse<CompanyNames>(companyNameString, true, out var companyName))
                {
                    companyNames.Add(companyName);
                }
                else
                {
                    // Optionally handle the error if a specific company name cannot be parsed
                    Console.WriteLine($"Unable to parse '{companyNameString}' to CompanyNames enum.");
                }
            }

            return companyNames;

        }
    }
}
