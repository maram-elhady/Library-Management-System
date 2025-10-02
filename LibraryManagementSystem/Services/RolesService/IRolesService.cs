using LibraryManagementSystem.Dtos.Auth;
using LibraryManagementSystem.Models.Authentacation;

namespace LibraryManagementSystem.Services.AuthService
{
    public interface IRolesService
    {
        Task<AuthModel> RegisterAsync(RegisterDto model);
        Task<AuthModel> LoginAsync(LoginDto model);
        Task<string> AddRoleAsync(AddRoleDto model);
    }
}
