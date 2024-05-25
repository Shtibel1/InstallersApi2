using BLL.Models;
using BLL.Services.AuthService;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IAuthService
    {

        Task<SignupServiceResponse?> SignupAsync(SignupModel signUp);
        Task<AppUserVm?> LoginAsync(LoginModel login);
        Task<bool> DeleteAsync(string Id);
    }
}
