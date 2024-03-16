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
        private readonly IHostEnvironment _env;
        public AuthController(IAuthService authService, IHostEnvironment env, IConfiguration configuration)
        {
            this._authService = authService;
            this._configuration = configuration;
            this._response = new();
            _env = env;
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
            _response.IsSuccess = true;
            _response.Message = "You have logged in successfully.";
            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("updateprofile")]
        //[Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProfile([FromForm] RegistrationDto model)
        {
            var image = Request.Form.Files.First();
            var uniqueFileName = GetUniqueFileName(image.FileName);
            var dir = Path.Combine(_env.ContentRootPath, "UserProfile");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var filePath = Path.Combine(dir, uniqueFileName);
            await image.CopyToAsync(new FileStream(filePath, FileMode.Create));
            model.ImageName = uniqueFileName;
            var errorMessage = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            _response.IsSuccess = true;
            _response.Message = "Your profile has been updated successfully.";
            _response.Result = model;
            return Ok(_response);
        }

        [HttpPost]
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_"
                   + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }

        private void SaveImagePathToDb(string description, string filepath)
        {
            //todo: description and file path to db
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
