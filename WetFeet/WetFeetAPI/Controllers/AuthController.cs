using Business.IServices;
using Business.Services;
using Data.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WetFeetAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        protected readonly ResponseDto _response;

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            this._authService = authService;
            this._configuration = configuration;
            this._response = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDto registrationDto)
        {
            var errorMessage = await _authService.Register(registrationDto);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            _response.Result = registrationDto;
            await _authService.AssignRole(registrationDto.Email, registrationDto.Role.ToUpper());
            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var loginResponse = await _authService.Login(loginDto);
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or password is incorrect";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("assignrole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationDto registrationDto)
        {
            var result = await _authService.AssignRole(registrationDto.Email, registrationDto.Role.ToUpper());
            if (!result)
            {
                _response.IsSuccess = false;
                _response.Message = "Error occured";
                return BadRequest(_response);
            }
            _response.Result = registrationDto;
            return Ok(_response);
        }
    }
}
