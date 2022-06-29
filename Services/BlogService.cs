using Services.Converters;
using Services.Interfaces;
using Repositories.Interfaces;
using Resources.Models;

namespace Services
{
    internal class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<List<BlogModel>> GetAllBlogModelsAsync()
        {
            var blogs = (await _blogRepository.GetAllBlogEntitiesAsync())
                .Select(obj => obj.BlogEntityToModel())
                .ToList();

            return blogs;
        }

        public async Task<List<BlogModel>> GetPublishedBlogModelsAsync()
        {
            var blogs = (await _blogRepository.GetAllBlogEntitiesAsync())
                .Where(b => b.IsPublished == true)
                .Select(b => b.BlogEntityToModel())
                .ToList();

            return blogs;
        }

        public async Task<BlogModel?> GetBlogByIdAsync(int projectId)
        {
            var blog = await _blogRepository.GetEntity(projectId);

            return blog?.BlogEntityToModel();
        }
    }
}
