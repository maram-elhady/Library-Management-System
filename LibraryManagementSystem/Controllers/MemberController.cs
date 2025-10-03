using LibraryManagementSystem.Dtos.MemeberDtos;
using LibraryManagementSystem.Services.MemeberService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator,Librarian")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _memberService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetById(string email)
        {
            var result = await _memberService.GetByIdAsync(email);
            if (result == null) return NotFound("Member not found");
            return Ok(result);
        }

        [HttpPut("{email}")]
        public async Task<IActionResult> Update(string email, UpdateMemberDto dto)
        {
            var result = await _memberService.UpdateAsync(email, dto);
            return Ok(result);
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            var result = await _memberService.DeleteAsync(email);
            return Ok(result);
        }
    }
}
