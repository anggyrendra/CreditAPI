using CreditAPI.DTOs;
using CreditAPI.Dtos;
using CreditAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CreditAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var token = _authService.Authenticate(dto.Username, dto.Password);
            if (token == null)
                return Unauthorized(new { message = "Username atau password salah" });

            return Ok(new { token });
        }
    }
}
