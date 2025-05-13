using Core.OAuth.Api.Models;
using Core.OAuth.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Core.OAuth.Api.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(AuthRequest request)
        {
            var result = await _authService.Register(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(AuthRequest request)
        {
            var result = await _authService.Register(request);
            if (!result.Success)
            {
                return Unauthorized(result.Message);
            }
            return Ok(result);
        }
    }
}
