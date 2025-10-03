namespace LibraryManagementSystem.Dtos.SearchDtos
{
    public class BookResultDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public int? PublicationYear { get; set; }
        public string ISBN { get; set; }
        public string Edition { get; set; }
        public string Summary { get; set; }
        public string CoverImagePath { get; set; }
        public string Status { get; set; }
        public string PublisherName { get; set; }
        public List<string> Authors { get; set; } = new();
        public List<string> Categories { get; set; } = new();
    }
}
