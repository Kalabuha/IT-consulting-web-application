using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace WebAppForGuests.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IHeaderService _headerService;

        public HeaderViewComponent(IHeaderService headerService)
        {
            _headerService = headerService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var headerModel = await _headerService.GetPublishedHeaderModelAsync();

            return View("Header", headerModel);
        }
    }
}
