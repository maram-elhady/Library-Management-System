using LibraryManagementSystem.Dtos.BorrowDtos;

namespace LibraryManagementSystem.Services.BorrowService
{
    public interface IBorrowService
    {
        Task<BorrowDto?> CreateAsync(CreateBorrowDto dto);
        Task<BorrowDto?> GetByIdAsync(int id);
        Task<IEnumerable<BorrowDto>> GetAllAsync();
        Task<IEnumerable<BorrowDto>> GetByMemberAsync(string memberId);
        Task<string> ReturnBookAsync(int id);
        Task<string> UpdateAsync(int id, UpdateBorrowDto dto);
        Task<string> DeleteAsync(int id);
    }
}
