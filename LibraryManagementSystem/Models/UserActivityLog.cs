namespace LibraryManagementSystem.Models
{
    public class UserActivityLog
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = new ApplicationUser();

        public string Action { get; set; } = string.Empty;
        public string EntityType { get; set; } = string.Empty;
        public string EntityId { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
