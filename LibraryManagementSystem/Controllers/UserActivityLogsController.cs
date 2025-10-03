using LibraryManagementSystem.Dtos.UserActivityDtos;
using LibraryManagementSystem.Services.UserActivityService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class UserActivityLogsController : ControllerBase
    {
        private readonly IUserActivityLogService _logService;

        public UserActivityLogsController(IUserActivityLogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLogs()
        {
            var logs = await _logService.GetAllLogsAsync();
            return Ok(logs);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetLogsByUser(string userId)
        {
            var logs = await _logService.GetLogsByUserAsync(userId);
            return Ok(logs);
        }

        [HttpPost("log")]
        [Authorize] // any authenticated user can log his action
        public async Task<IActionResult> LogActivity([FromBody] CreateUserActivityLogDto dto)
        {
            var userId = User.FindFirst("uid")?.Value;
            var log = await _logService.LogActivityAsync(userId, dto);
            return Ok(log);
        }
    }
}
