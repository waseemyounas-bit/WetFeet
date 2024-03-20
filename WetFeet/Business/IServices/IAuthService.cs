using Data.Dtos;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IServices
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginDto loginDto);
        Task<String> Register(RegistrationDto registrationDto);
        Task<String> UpdateUser(RegistrationDto registrationDto);
        Task<bool> AssignRole(string email, string role);
        Task<ApplicationUser> GetUserIdByEmail(string email);
    }
}
