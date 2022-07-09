using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using WebAppForGuests.Models;

namespace WebAppForGuests.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IHeaderService _headerService;

        public ContactsController(IContactService contactService, IHeaderService headerService)
        {
            _contactService = contactService;
            _headerService = headerService;
        }


        public async Task<ActionResult<ContactsViewModel>> Index()
        {
            var headerModel = await _headerService.GetPublishedHeaderModelAsync();
            ViewBag.PageH1 = headerModel.Contacts;

            var contact = await _contactService.GetPublishedContactModelAsync();

            return View(new ContactsViewModel { Contact = contact });
        }
    }
}
