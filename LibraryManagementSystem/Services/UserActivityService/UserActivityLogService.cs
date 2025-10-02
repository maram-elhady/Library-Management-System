using LibraryManagementSystem.Dtos.UserActivityDtos;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services.UserActivityService
{
    public class UserActivityLogService : IUserActivityLogService
    {
        private readonly ApplicationDbContext _context;

        public UserActivityLogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserActivityLogDto> LogActivityAsync(string userId, CreateUserActivityLogDto dto)
        {
            var log = new UserActivityLog
            {
                UserId = userId,
                Action = dto.Action,
                EntityType = dto.EntityType,
                EntityId = dto.EntityId,
                Details = dto.Details,
                CreatedAt = DateTime.UtcNow
            };

            _context.UserActivityLogs.Add(log);
            await _context.SaveChangesAsync();

            return new UserActivityLogDto
            {
                Id = log.Id,
                UserId = log.UserId,
                Action = log.Action,
                EntityType = log.EntityType,
                EntityId = log.EntityId,
                Details = log.Details,
                CreatedAt = log.CreatedAt
            };
        }

        public async Task<IEnumerable<UserActivityLogDto>> GetAllLogsAsync()
        {
            return await _context.UserActivityLogs
                .Select(l => new UserActivityLogDto
                {
                    Id = l.Id,
                    UserId = l.UserId,
                    Action = l.Action,
                    EntityType = l.EntityType,
                    EntityId = l.EntityId,
                    Details = l.Details,
                    CreatedAt = l.CreatedAt
                }).ToListAsync();
        }

        public async Task<IEnumerable<UserActivityLogDto>> GetLogsByUserAsync(string userId)
        {
            return await _context.UserActivityLogs
                .Where(l => l.UserId == userId)
                .Select(l => new UserActivityLogDto
                {
                    Id = l.Id,
                    UserId = l.UserId,
                    Action = l.Action,
                    EntityType = l.EntityType,
                    EntityId = l.EntityId,
                    Details = l.Details,
                    CreatedAt = l.CreatedAt
                }).ToListAsync();
        }
    }
}
