using LibraryManagementSystem.Dtos.MemeberDtos;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services.MemeberService
{
    public class MemberService : IMemberService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public MemberService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<MemberDto>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var members = new List<MemberDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Member"))
                {
                    members.Add(new MemberDto
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        Email = user.Email!,
                        phoneNumber=user.PhoneNumber,
                        Role = "Member"
                    });
                }
            }

            return members;
        }

        public async Task<MemberDto?> GetByIdAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("Member")) return null;

            return new MemberDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email!,
                Role = "Member"
            };
        }

        public async Task<string> UpdateAsync(string email, UpdateMemberDto dto)
        {
            var user = await _userManager.FindByIdAsync(email);
            if (user == null) return "Member not found";

            user.FullName = dto.FullName;
            user.Email = dto.Email;
            user.PhoneNumber = dto.phoneNumber;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded ? "Member updated successfully" : "Update failed";
        }

        public async Task<string> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return "Member not found";

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded ? "Member deleted successfully" : "Delete failed";
        }
    }
}
