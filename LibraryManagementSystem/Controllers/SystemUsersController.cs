using LibraryManagementSystem.Dtos.SystemUserDtos;
using LibraryManagementSystem.Services.SystemUserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class SystemUsersController : ControllerBase
    {
        private readonly ISystemUserService _systemUserService;

        public SystemUsersController(ISystemUserService systemUserService)
        {
            _systemUserService = systemUserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _systemUserService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _systemUserService.GetByIdAsync(id);
            if (result == null) return NotFound("System user not found");
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateSystemUserDto dto)
        {
            var result = await _systemUserService.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _systemUserService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
