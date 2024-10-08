using ChatApp.Models;
using ChatApp.Services;
using ChatApp.Services.Helpers;
using ChatApp.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ReturnModel> Login([FromBody] LoginModel loginModel)
        {
            var user = await _userService.GetByUsernameAndPasswordAsync(loginModel.Username, loginModel.Password);
            if (user == null)
            {
                return new ReturnModel
                {
                    Success = false,
                    Message = "Invalid username or password",
                    StatusCode = 400
                };
            }

            var tokenModel = new TokenModel
            {
                Username = user.Username,
                Role = user.Role,
                SigninKey = _configuration["Jwt:SigningKey"],
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
            };
            var token = JwtToken.GenerateToken(tokenModel);
            return new ReturnModel
            {
                Success = true,
                Message = "Login Successfully",
                Data = token,
                StatusCode = 200
            };
        }
    }
}