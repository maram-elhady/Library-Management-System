using LibraryManagementSystem.Dtos.SystemUserDtos;

namespace LibraryManagementSystem.Services.SystemUserService
{
    public interface ISystemUserService
    {
        Task<IEnumerable<SystemUserDto>> GetAllAsync();
        Task<SystemUserDto?> GetByIdAsync(string id);
        Task<string> UpdateAsync(string id, UpdateSystemUserDto dto);
        Task<string> DeleteAsync(string id);
    }
}
