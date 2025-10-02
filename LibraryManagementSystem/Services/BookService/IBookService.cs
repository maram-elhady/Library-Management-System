using LibraryManagementSystem.Dtos.BookDtos;

namespace LibraryManagementSystem.Services.BookService
{
    public interface IBookService
    {

        Task<BookDto> CreateAsync(BookCreateDto dto);
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task<BookDto?> GetByIdAsync(int id);
        Task<BookDto?> UpdateAsync(int id, BookUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
