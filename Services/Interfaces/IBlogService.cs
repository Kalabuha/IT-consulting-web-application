using Resources.Models;

namespace Services.Interfaces
{
    public interface IBlogService
    {
        public Task<List<BlogModel>> GetAllBlogModelsAsync();
        public Task<List<BlogModel>> GetPublishedBlogModelsAsync();
        public Task<BlogModel?> GetBlogByIdAsync(int projectId);
    }
}
