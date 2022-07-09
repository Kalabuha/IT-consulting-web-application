using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using WebAppForGuests.Models;

namespace WebAppForGuests.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IHeaderService _headerService;

        public BlogsController(IBlogService blogService, IHeaderService headerService)
        {
            _blogService = blogService;
            _headerService = headerService;
        }

        [HttpGet]
        public async Task<ActionResult<BlogsViewModel>> Index()
        {
            var headerModel = await _headerService.GetPublishedHeaderModelAsync();
            ViewBag.PageH1 = headerModel.Blogs;

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
