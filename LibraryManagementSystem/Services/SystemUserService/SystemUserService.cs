using LibraryManagementSystem.Dtos.SystemUserDtos;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services.SystemUserService
{
    public class SystemUserService : ISystemUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SystemUserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<SystemUserDto>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var systemUsers = new List<SystemUserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (!roles.Contains("Member")) 
                {
                    systemUsers.Add(new SystemUserDto
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        Email = user.Email!,
                        Role = roles.FirstOrDefault() ?? "No Role"
                    });
                }
            }

            return systemUsers;
        }

        public async Task<SystemUserDto?> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("Member")) return null; 

            return new SystemUserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email!,
                Role = roles.FirstOrDefault() ?? "No Role"
            };
        }

        public async Task<string> UpdateAsync(string id, UpdateSystemUserDto dto)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return "System user not found";

            user.FullName = dto.FullName;
            user.Email = dto.Email;
            user.PhoneNumber = dto.phoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return "Update failed";

            var currentRoles = await _userManager.GetRolesAsync(user);

            if (!string.IsNullOrEmpty(dto.Role) && !currentRoles.Contains(dto.Role))
            {
                // remove old roles
                await _userManager.RemoveFromRolesAsync(user, currentRoles);

                // add new role
                if (await _roleManager.RoleExistsAsync(dto.Role))
                    await _userManager.AddToRoleAsync(user, dto.Role);
            }

            return "System user updated successfully";
        }

        public async Task<string> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return "System user not found";

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded ? "System user deleted successfully" : "Delete failed";
        }
    }
}
