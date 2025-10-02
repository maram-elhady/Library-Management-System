namespace LibraryManagementSystem.Dtos.BookDtos
{
    public class BookDto
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
        public string PublisherName { get; set; } = string.Empty;

        public List<string> Authors { get; set; } = new();
        public List<string> Categories { get; set; } = new();
    }
}
