using AuthService.DTOs;
using AuthService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthController(
            IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterRequest request)
        {
            var isCreated =
                await _authService.RegisterAsync(request);

            if (!isCreated)
            {
                return Conflict(new
                {
                    Message =
                        "Bu kullanıcı adı zaten kullanılıyor."
                });
            }

            return StatusCode(
                StatusCodes.Status201Created,
                new
                {
                    Message = "Kullanıcı başarıyla oluşturuldu"
                });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginRequest request)
        {
            var result =
                await _authService.LoginAsync(request);

            if (result is null)
            {
                return Unauthorized(new
                {
                    Message =
                        "Kullanıcı adı veya şifre hatalı."
                });
            }

            return Ok(result);
        }
    }
}