namespace LibraryManagementSystem.Models
{
    public class Publisher
    {
        public int PublisherId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Country { get; set; }
        public string? Email { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
