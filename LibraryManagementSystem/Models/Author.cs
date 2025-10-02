namespace LibraryManagementSystem.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string FullName { get; set; } = string.Empty;

        public ICollection<BookAuthor> BookAuthors { get; set; } 
    }
}
