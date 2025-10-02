namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public int? PublicationYear { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Edition { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string CoverImagePath { get; set; } = string.Empty;
        public string Status { get; set; } = "Available";

        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; } 

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
        public ICollection<BorrowTransaction> BorrowTransactions { get; set; } = new List<BorrowTransaction>();
    }
}
