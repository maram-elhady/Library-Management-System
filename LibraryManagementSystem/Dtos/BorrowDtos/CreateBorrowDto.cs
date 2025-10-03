namespace LibraryManagementSystem.Dtos.BorrowDtos
{
    public class CreateBorrowDto
    {
        public int BookId { get; set; }
        public string MemberId { get; set; }
        public string UserId { get; set; } = string.Empty; //who registered data
        public DateTime DueDate { get; set; }
    }
}
