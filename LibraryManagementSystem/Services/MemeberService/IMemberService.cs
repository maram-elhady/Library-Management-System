using LibraryManagementSystem.Dtos.MemeberDtos;

namespace LibraryManagementSystem.Services.MemeberService
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberDto>> GetAllAsync();
        Task<MemberDto?> GetByIdAsync(string email);
        Task<string> UpdateAsync(string email, UpdateMemberDto dto);
        Task<string> DeleteAsync(string email);
    }
}
