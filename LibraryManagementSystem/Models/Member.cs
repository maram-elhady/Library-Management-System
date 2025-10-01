namespace LibraryManagementSystem.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }

        public ICollection<BorrowTransaction> BorrowTransactions { get; set; } = new List<BorrowTransaction>();
    }
}
