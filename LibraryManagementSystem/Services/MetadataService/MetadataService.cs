using LibraryManagementSystem.Dtos.MetadataDtos;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services.MetadataService
{
    public class MetadataService : IMetadataService
    {
        private readonly ApplicationDbContext _context;

        public MetadataService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuthorDto>> GetAuthorsAsync()
        {
            return await _context.Authors
                .Select(a => new AuthorDto
                {
                    AuthorId = a.AuthorId,
                    FullName = a.FullName
                }).ToListAsync();
        }

        public async Task<IEnumerable<PublisherDto>> GetPublishersAsync()
        {
            return await _context.Publishers
                .Select(p => new PublisherDto
                {
                    PublisherId = p.PublisherId,
                    Name = p.Name,
                    Country = p.Country,
                    Email = p.Email
                }).ToListAsync();
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name,
                    ParentCategoryId = c.ParentCategoryId
                }).ToListAsync();
        }

    }
}
