namespace LibraryManagementSystem.Models
{
    public class BorrowTransaction
    {
        public int BorrowTransactionId { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; } = new Book();

        public int MemberId { get; set; }
        public Member Member { get; set; } = new Member();

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = new ApplicationUser();

        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; } = "Borrowed";
    }
}
