using AutoMapper;
using Business.IServices;
using Business.Services;
using Data.Dtos;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;
using static Data.WetFeetEnums;

namespace WetFeetAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        protected readonly ResponseDto _response;

        public AuthController(IAuthService authService, ISubscriptionService subscriptionService , IConfiguration configuration,
            IMapper mapper)
        {
            this._authService = authService;
            this._subscriptionService = subscriptionService;
            this._configuration = configuration;
            this._mapper = mapper;
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

        [HttpPost("registeraudience")]
        public async Task<IActionResult> RegisterAudience([FromBody] RegistrationAudienceDto model)
        {
            var result = await _authService.Register(model.RegistrationDto);
            if (!string.IsNullOrEmpty(result))
            {
                _response.IsSuccess = false;
                _response.Message = result;
                return BadRequest(_response);
            }
            _response.Result = model;
            await _authService.AssignRole(model.RegistrationDto.Email, model.RegistrationDto.Role.ToUpper());

            var userId = await _authService.GetUserIdByEmail(model.RegistrationDto.Email);
            var subscriptionPlan = this._subscriptionService.GetSubscriptionPlan(model.SubscriptionId);
            var amount = model.Type == SubscriptionType.Monthly ? subscriptionPlan.MonthlyAmount : subscriptionPlan.YearlyAmount;
            var userSubscriptionPlan = new UserSubscriptionPlan
            {
                SubscriptionId = subscriptionPlan.Id,
                Amount = amount,
                Type = model.Type,
                UserId = userId,
                IsActive = true,
                ActivatedDate = DateTime.UtcNow
            };
            this._subscriptionService.AddUserSubscriptionPlan(userSubscriptionPlan);
            
            return Ok(_response);
        }

        [HttpGet("getallsubscriptions")]
        public IActionResult GetAllSubscriptions()
        {
            try
            {
                var objList = this._subscriptionService.GetAllSubscriptionPlans();
                _response.Result = _mapper.Map<List<SubscriptionPlanDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }
    }
}
