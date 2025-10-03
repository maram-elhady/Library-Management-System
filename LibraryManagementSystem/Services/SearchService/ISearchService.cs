using LibraryManagementSystem.Dtos.SearchDtos;

namespace LibraryManagementSystem.Services.SearchService
{
    public interface ISearchService
    {
        Task<IEnumerable<BookResultDto>> SearchAsync(BookSearchDto dto);
        Task<IEnumerable<BookResultDto>> GetBooksByStatusAsync(string status);
    }
}
