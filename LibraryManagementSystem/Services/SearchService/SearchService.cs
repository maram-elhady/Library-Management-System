using LibraryManagementSystem.Dtos.SearchDtos;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services.SearchService
{
    public class SearchService : ISearchService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SearchService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<BookResultDto>> SearchAsync(BookSearchDto dto)
        {
            var query = _context.Books
                .Include(b => b.Publisher)
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .Include(b => b.BookCategories).ThenInclude(bc => bc.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(dto.Title))
            {
                query = query.Where(b => b.Title.Contains(dto.Title));
            }

            if (!string.IsNullOrEmpty(dto.AuthorName))
            {
                query = query.Where(b => b.BookAuthors.Any(ba => ba.Author.FullName.Contains(dto.AuthorName)));
            }

            if (!string.IsNullOrEmpty(dto.CategoryName))
            {
                query = query.Where(b => b.BookCategories.Any(bc => bc.Category.Name.Contains(dto.CategoryName)));
            }

            var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/";

            return await query.Select(b => new BookResultDto
            {
                BookId = b.BookId,
                Title = b.Title,
                Language = b.Language,
                PublicationYear = b.PublicationYear,
                ISBN = b.ISBN,
                Edition = b.Edition,
                Summary = b.Summary,
                CoverImagePath = string.IsNullOrEmpty(b.CoverImagePath) ? "" : baseUrl + b.CoverImagePath,
                Status = b.Status,
                PublisherName = b.Publisher.Name,
                Authors = b.BookAuthors.Select(ba => ba.Author.FullName).ToList(),
                Categories = b.BookCategories.Select(bc => bc.Category.Name).ToList()
            }).ToListAsync();
        }

        public async Task<IEnumerable<BookResultDto>> GetBooksByStatusAsync(string status)
        {
            return await _context.Books
                .Include(b => b.Publisher)
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .Include(b => b.BookCategories).ThenInclude(bc => bc.Category)
                .Where(b => b.Status == status)
                .Select(b => new BookResultDto
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Language = b.Language,
                    PublicationYear = b.PublicationYear,
                    ISBN = b.ISBN,
                    Edition = b.Edition,
                    Summary = b.Summary,
                    CoverImagePath = b.CoverImagePath,
                    Status = b.Status,
                    PublisherName = b.Publisher.Name,
                    Authors = b.BookAuthors.Select(ba => ba.Author.FullName).ToList(),
                    Categories = b.BookCategories.Select(bc => bc.Category.Name).ToList()
                })
                .ToListAsync();
        }

    }
}
