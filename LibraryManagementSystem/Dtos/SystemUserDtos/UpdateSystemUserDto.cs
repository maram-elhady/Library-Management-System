namespace LibraryManagementSystem.Dtos.SystemUserDtos
{
    public class UpdateSystemUserDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string phoneNumber { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
