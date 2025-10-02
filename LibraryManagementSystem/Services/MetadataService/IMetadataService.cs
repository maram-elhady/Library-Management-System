using LibraryManagementSystem.Dtos.MetadataDtos;

namespace LibraryManagementSystem.Services.MetadataService
{
    public interface IMetadataService
    {
        Task<IEnumerable<AuthorDto>> GetAuthorsAsync();
        Task<IEnumerable<PublisherDto>> GetPublishersAsync();
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    }
}
