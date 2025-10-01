namespace LibraryManagementSystem.Models
{
    public class BookCategory
    {
        public int BookId { get; set; }
        public Book Book { get; set; } = new Book();

        public int CategoryId { get; set; }
        public Category Category { get; set; } = new Category();
    }
}
