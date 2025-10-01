using LibraryManagementSystem.Dtos;
using LibraryManagementSystem.Models.Authentacation;

namespace LibraryManagementSystem.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterDto model);
        Task<AuthModel> LoginAsync(LoginDto model);
        Task<string> AddRoleAsync(AddRoleDto model);
    }
}
