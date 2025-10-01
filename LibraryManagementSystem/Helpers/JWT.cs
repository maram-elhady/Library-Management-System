namespace LibraryManagementSystem.Helpers
{
    public class JWT
    {
        public string Key { get; set; } = string.Empty;
        public string Issue { get; set; } = string.Empty;
        public string Audeince { get; set; } = string.Empty;
        public double DurationInDays { get; set; }
    }
}
