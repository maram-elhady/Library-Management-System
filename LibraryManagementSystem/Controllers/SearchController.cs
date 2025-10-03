using LibraryManagementSystem.Dtos.SearchDtos;
using LibraryManagementSystem.Services.BookService;
using LibraryManagementSystem.Services.SearchService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchBooks([FromBody] BookSearchDto dto)
        {
            var result = await _searchService.SearchAsync(dto);
            return Ok(result);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetBooksByStatus(string status)
        {
            var books = await _searchService.GetBooksByStatusAsync(status);

            if (!books.Any())
                return NotFound(new { message = $"No books found with status '{status}'" });

            return Ok(books);
        }
    }
}
