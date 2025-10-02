using LibraryManagementSystem.Dtos.UserActivityDtos;

namespace LibraryManagementSystem.Services.UserActivityService
{
    public interface IUserActivityLogService
    {
        Task<UserActivityLogDto> LogActivityAsync(string userId, CreateUserActivityLogDto dto);
        Task<IEnumerable<UserActivityLogDto>> GetAllLogsAsync();
        Task<IEnumerable<UserActivityLogDto>> GetLogsByUserAsync(string userId);
    }
}
