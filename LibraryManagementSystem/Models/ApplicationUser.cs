using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;

        // Navigation
        public ICollection<UserActivityLog> ActivityLogs { get; set; } = new List<UserActivityLog>();
        public ICollection<BorrowTransaction> BorrowTransactions { get; set; } = new List<BorrowTransaction>(); 

    }
}
