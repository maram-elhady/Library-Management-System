using LibraryManagementSystem.Dtos.BorrowDtos;
using LibraryManagementSystem.Services.BorrowService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Administrator,Librarian")]
    public class BorrowController : ControllerBase
    {
        private readonly IBorrowService _borrowService;

        public BorrowController(IBorrowService borrowService)
        {
            _borrowService = borrowService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBorrowDto dto)
        {
            var result = await _borrowService.CreateAsync(dto);
            if (result == null) return BadRequest("Book not available or invalid data");
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _borrowService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _borrowService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("member/{memberId}")]
        public async Task<IActionResult> GetByMember(int memberId)
        {
            var result = await _borrowService.GetByMemberAsync(memberId);
            return Ok(result);
        }

        [HttpPut("return/{id}")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var result = await _borrowService.ReturnBookAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBorrowDto dto)
        {
            var result = await _borrowService.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _borrowService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
