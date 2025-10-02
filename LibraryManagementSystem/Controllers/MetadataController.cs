using LibraryManagementSystem.Services.MetadataService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetadataController : ControllerBase
    {

        private readonly IMetadataService _metadataService;

        public MetadataController(IMetadataService metadataService)
        {
            _metadataService = metadataService;
        }

        [HttpGet("authors")]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _metadataService.GetAuthorsAsync();
            return Ok(authors);
        }

        [HttpGet("publishers")]
        public async Task<IActionResult> GetPublishers()
        {
            var publishers = await _metadataService.GetPublishersAsync();
            return Ok(publishers);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _metadataService.GetCategoriesAsync();
            return Ok(categories);
        }
    }
}
