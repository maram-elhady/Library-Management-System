using LibraryManagementSystem.Dtos;
using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly IAuthService _authService;
        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
            //return Ok(new {token=result.Token,expiresOn=result.ExpiresOn});
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
            //return Ok(new {token=result.Token,expiresOn=result.ExpiresOn});

        }
        [Authorize(Roles = "Administrator")]
        [HttpPost("addRole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);


        }

    }
}
