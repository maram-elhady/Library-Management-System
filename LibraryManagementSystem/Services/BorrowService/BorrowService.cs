using LibraryManagementSystem.Dtos.BorrowDtos;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services.BorrowService
{
    public class BorrowService : IBorrowService
    {
        private readonly ApplicationDbContext _context;

        public BorrowService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BorrowDto?> CreateAsync(CreateBorrowDto dto)
        {
            var book = await _context.Books.FindAsync(dto.BookId);
            if (book == null || book.Status != "Available")
                return null;

            var borrow = new BorrowTransaction
            {
                BookId = dto.BookId,
                MemberId=dto.MemberId,
                UserId = dto.UserId,
                BorrowDate = DateTime.UtcNow,
                DueDate = dto.DueDate,
                Status = "Borrowed"
            };

            book.Status = "Borrowed";

            _context.BorrowTransactions.Add(borrow);
            await _context.SaveChangesAsync();

            return new BorrowDto
            {
                BorrowTransactionId = borrow.BorrowTransactionId,
                BookId = book.BookId,
                BookTitle = book.Title,
                MemberId = dto.MemberId,
                MemberName = (await _context.Users.FindAsync(dto.MemberId))?.FullName ?? "Unknown",
                BorrowDate = borrow.BorrowDate,
                DueDate = borrow.DueDate,
                Status = borrow.Status
            };
        }

        public async Task<BorrowDto?> GetByIdAsync(int id)
        {
            var borrow = await _context.BorrowTransactions
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.BorrowTransactionId == id);

            if (borrow == null) return null;

            return new BorrowDto
            {
                BorrowTransactionId = borrow.BorrowTransactionId,
                BookId = borrow.BookId,
                BookTitle = borrow.Book.Title,
                MemberId = borrow.MemberId,
                MemberName = borrow.User.FullName,
                BorrowDate = borrow.BorrowDate,
                DueDate = borrow.DueDate,
                ReturnDate = borrow.ReturnDate,
                Status = borrow.Status
            };
        }

        public async Task<IEnumerable<BorrowDto>> GetAllAsync()
        {
            return await _context.BorrowTransactions
                .Include(b => b.Book)
                .Include(b => b.User)
                .Select(b => new BorrowDto
                {
                    BorrowTransactionId = b.BorrowTransactionId,
                    BookId = b.BookId,
                    BookTitle = b.Book.Title,
                    MemberId = b.MemberId,
                    MemberName = b.User.FullName,
                    BorrowDate = b.BorrowDate,
                    DueDate = b.DueDate,
                    ReturnDate = b.ReturnDate,
                    Status = b.Status
                }).ToListAsync();
        }

        public async Task<IEnumerable<BorrowDto>> GetByMemberAsync(string memberId)
        {
            return await _context.BorrowTransactions
                .Include(b => b.Book)
                .Include(b => b.User)
                .Where(b => b.MemberId == memberId)
                .Select(b => new BorrowDto
                {
                    BorrowTransactionId = b.BorrowTransactionId,
                    BookId = b.BookId,
                    BookTitle = b.Book.Title,
                    MemberId = b.MemberId,
                    MemberName = b.User.FullName,
                    BorrowDate = b.BorrowDate,
                    DueDate = b.DueDate,
                    ReturnDate = b.ReturnDate,
                    Status = b.Status
                }).ToListAsync();
        }

        public async Task<string> ReturnBookAsync(int id)
        {
            var borrow = await _context.BorrowTransactions
                .Include(b => b.Book)
                .FirstOrDefaultAsync(b => b.BorrowTransactionId == id);

            if (borrow == null) return "Transaction not found";
            if (borrow.Status == "Returned") return "Book already returned";

            borrow.ReturnDate = DateTime.UtcNow;
            borrow.Status = "Returned";

            borrow.Book.Status = "Available";

            await _context.SaveChangesAsync();
            return "Book returned successfully";
        }

        public async Task<string> UpdateAsync(int id, UpdateBorrowDto dto)
        {
            var borrow = await _context.BorrowTransactions.FindAsync(id);
            if (borrow == null) return "Transaction not found";

            borrow.DueDate = dto.DueDate;
            await _context.SaveChangesAsync();

            return "Borrow transaction updated successfully";
        }

        public async Task<string> DeleteAsync(int id)
        {
            var borrow = await _context.BorrowTransactions.FindAsync(id);
            if (borrow == null) return "Transaction not found";

            _context.BorrowTransactions.Remove(borrow);
            await _context.SaveChangesAsync();

            return "Borrow transaction deleted successfully";
        }

    }
}
