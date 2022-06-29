using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using WebAppForGuests.Models;

namespace WebAppForGuests.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<ActionResult<BlogsViewModel>> Index()
        {
            var blogs = await _blogService.GetPublishedBlogModelsAsync();

            return View(new BlogsViewModel { Blogs = blogs });
        }

        [HttpGet]
        public async Task<ActionResult<BlogsViewModel>> BlogDetails(int id)
        {
            var blog = await _blogService.GetBlogByIdAsync(id);

            return View(new BlogsViewModel { SelectedBlog = blog });
        }
    }
}
