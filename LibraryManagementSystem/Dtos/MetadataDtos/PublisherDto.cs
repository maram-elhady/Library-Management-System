﻿namespace LibraryManagementSystem.Dtos.MetadataDtos
{
    public class PublisherDto
    {
        public int PublisherId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Country { get; set; }
        public string? Email { get; set; }
    }
}
