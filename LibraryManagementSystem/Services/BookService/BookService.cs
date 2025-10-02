using Azure.Core;
using LibraryManagementSystem.Dtos.BookDtos;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BookService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<BookDto> CreateAsync(BookCreateDto dto)
        {
            string? imagePath = null;

            if (dto.CoverImage != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "books");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid()}_{dto.CoverImage.FileName}";
                imagePath = Path.Combine("books", fileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.CoverImage.CopyToAsync(stream);
                }

            }
            var book = new Book
            {
                Title = dto.Title,
                Language = dto.Language,
                PublicationYear = dto.PublicationYear,
                ISBN = dto.ISBN,
                Edition = dto.Edition,
                Summary = dto.Summary,
                CoverImagePath = imagePath,
                PublisherId = dto.PublisherId
            };
            foreach (var authorId in dto.AuthorIds)
            {
                var authorExists = await _context.Authors.AnyAsync(a => a.AuthorId == authorId);
                if (authorExists)
                {
                    book.BookAuthors.Add(new BookAuthor { AuthorId = authorId });
                }
            }

            
            foreach (var categoryId in dto.CategoryIds)
            {
                var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == categoryId);
                if (categoryExists)
                {
                    book.BookCategories.Add(new BookCategory { CategoryId = categoryId });
                }
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(book.BookId) ?? throw new Exception("Book not found after creation");
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/";
            return await _context.Books
                .Include(b => b.Publisher)
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .Include(b => b.BookCategories).ThenInclude(bc => bc.Category)
                .Select(b => new BookDto
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Language = b.Language,
                    PublicationYear = b.PublicationYear,
                    ISBN = b.ISBN,
                    Edition = b.Edition,
                    Summary = b.Summary,
                    CoverImagePath = baseUrl+b.CoverImagePath,
                    Status = b.Status,
                    PublisherId = b.PublisherId,
                    PublisherName = b.Publisher.Name,
                    Authors = b.BookAuthors.Select(ba => ba.Author.FullName).ToList(),
                    Categories = b.BookCategories.Select(bc => bc.Category.Name).ToList()
                })
                .ToListAsync();
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var b = await _context.Books
                .Include(b => b.Publisher)
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .Include(b => b.BookCategories).ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (b == null) return null;
            var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/";
            return new BookDto
            {
                BookId = b.BookId,
                Title = b.Title,
                Language = b.Language,
                PublicationYear = b.PublicationYear,
                ISBN = b.ISBN,
                Edition = b.Edition,
                Summary = b.Summary,
                CoverImagePath = baseUrl+b.CoverImagePath,
                Status = b.Status,
                PublisherId = b.PublisherId,
                PublisherName = b.Publisher.Name,
                Authors = b.BookAuthors.Select(ba => ba.Author.FullName).ToList(),
                Categories = b.BookCategories.Select(bc => bc.Category.Name).ToList()
            };
        }

        public async Task<BookDto?> UpdateAsync(int id, BookUpdateDto dto)
        {
            var book = await _context.Books
                .Include(b => b.BookAuthors)
                .Include(b => b.BookCategories)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null) return null;

            if (dto.CoverImage != null)
            {
                if (!string.IsNullOrEmpty(book.CoverImagePath))
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", book.CoverImagePath);
                    if (File.Exists(oldPath))
                        File.Delete(oldPath);
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "books");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid()}_{dto.CoverImage.FileName}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.CoverImage.CopyToAsync(stream);
                }

                book.CoverImagePath = Path.Combine("books", fileName);
            }

            book.Title = dto.Title;
            book.Language = dto.Language;
            book.PublicationYear = dto.PublicationYear;
            book.ISBN = dto.ISBN;
            book.Edition = dto.Edition;
            book.Summary = dto.Summary;
            book.Status = dto.Status;
            book.PublisherId = dto.PublisherId;

            // Authors update
            book.BookAuthors.Clear();
            foreach (var authorId in dto.AuthorIds)
            {
                var authorExists = await _context.Authors.AnyAsync(a => a.AuthorId == authorId);
                if (authorExists)
                {
                    book.BookAuthors.Add(new BookAuthor { AuthorId = authorId });
                }
            }

            // Categories update
            book.BookCategories.Clear();
            foreach (var categoryId in dto.CategoryIds)
            {
                var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == categoryId);
                if (categoryExists)
                {
                    book.BookCategories.Add(new BookCategory { CategoryId = categoryId });
                }
            }

            await _context.SaveChangesAsync();
            return await GetByIdAsync(id);
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
