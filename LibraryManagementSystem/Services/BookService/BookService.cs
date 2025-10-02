using LibraryManagementSystem.Dtos.BookDtos;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<BookDto> CreateAsync(BookCreateDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Language = dto.Language,
                PublicationYear = dto.PublicationYear,
                ISBN = dto.ISBN,
                Edition = dto.Edition,
                Summary = dto.Summary,
                CoverImagePath = dto.CoverImagePath,
                PublisherId = dto.PublisherId
            };

            foreach (var authorId in dto.AuthorIds)
                book.BookAuthors.Add(new BookAuthor { AuthorId = authorId, Book = book });

            foreach (var categoryId in dto.CategoryIds)
                book.BookCategories.Add(new BookCategory { CategoryId = categoryId, Book = book });

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(book.BookId) ?? throw new Exception("Book not found after creation");
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
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
                    CoverImagePath = b.CoverImagePath,
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

            return new BookDto
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

            book.Title = dto.Title;
            book.Language = dto.Language;
            book.PublicationYear = dto.PublicationYear;
            book.ISBN = dto.ISBN;
            book.Edition = dto.Edition;
            book.Summary = dto.Summary;
            book.CoverImagePath = dto.CoverImagePath;
            book.Status = dto.Status;
            book.PublisherId = dto.PublisherId;

            // Authors update
            book.BookAuthors.Clear();
            foreach (var authorId in dto.AuthorIds)
                book.BookAuthors.Add(new BookAuthor { AuthorId = authorId, BookId = id });

            // Categories update
            book.BookCategories.Clear();
            foreach (var categoryId in dto.CategoryIds)
                book.BookCategories.Add(new BookCategory { CategoryId = categoryId, BookId = id });

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
