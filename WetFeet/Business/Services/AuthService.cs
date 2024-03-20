using Business.IServices;
using Data.Context;
using Data.Dtos;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthService(DataContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IJwtTokenService jwtTokenService)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._jwtTokenService = jwtTokenService;
        }
        public async Task<bool> AssignRole(string email, string role)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, role);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            var isValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (user == null || !isValid)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }

            //If user found, Generate JWT Token.
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenService.GenerateToken(user, roles);

            UserDto userDto = new()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Password = user.PasswordHash,
                About = user.About,
                ImageName = user.ImageName,
                Website = user.Website
                
            };

            var loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };
            return loginResponseDto;
        }

        public async Task<string> Register(RegistrationDto registrationDto)
        {
            ApplicationUser appUser = new()
            {
                FirstName = registrationDto.FirstName,
                LastName = registrationDto.LastName,
                UserName = registrationDto.Email,
                Email = registrationDto.Email,
                NormalizedEmail = registrationDto.Email.ToUpper(),
                PhoneNumber = registrationDto.PhoneNumber,
                ImageName = registrationDto.ImageName,
            };

            try
            {
                var result = await _userManager.CreateAsync(appUser, registrationDto.Password);
                if (result.Succeeded)
                {
                    var user = this._dbContext.ApplicationUsers.First(x => x.UserName == registrationDto.Email);

                    UserDto userDto = new()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        ImageName = user.ImageName,
                        Password = user.PasswordHash,
                        About = user.About
                    };

                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {

            }

            return "An Error Occured";
        }
        public async Task<string> UpdateUser(RegistrationDto registrationDto)
        {
            try
            {

                var user = this._dbContext.ApplicationUsers.First(x => x.UserName == registrationDto.Email);

                user.FirstName = registrationDto.FirstName;
                user.LastName = registrationDto.LastName;
                user.PhoneNumber = registrationDto.PhoneNumber;
                user.ImageName = registrationDto.ImageName;
                user.About = registrationDto.About;
                user.Website = registrationDto.Website;

                _dbContext.ApplicationUsers.Update(user);
                _dbContext.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<ApplicationUser> GetUserIdByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new ApplicationUser();
            return user;
        }
    }
}
