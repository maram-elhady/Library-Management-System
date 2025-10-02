namespace LibraryManagementSystem.Dtos.BookDtos
{
    public class BookUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public int? PublicationYear { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Edition { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public IFormFile? CoverImage { get; set; } 
        public string Status { get; set; } = "Available";
        public int PublisherId { get; set; }
        public List<int> AuthorIds { get; set; } = new();
        public List<int> CategoryIds { get; set; } = new();
    }
}
