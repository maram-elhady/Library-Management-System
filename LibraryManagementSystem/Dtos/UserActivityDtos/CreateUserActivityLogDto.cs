namespace LibraryManagementSystem.Dtos.UserActivityDtos
{
    public class CreateUserActivityLogDto
    {
        public string Action { get; set; } = string.Empty;
        public string EntityType { get; set; } = string.Empty;
        public string EntityId { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
    }
}
