using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using WebAppForGuests.Models;

namespace WebAppForGuests.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<ActionResult<ContactsViewModel>> Index()
        {
            var contact = await _contactService.GetPublishedContactModelAsync();

            return View(new ContactsViewModel { Contact = contact });
        }
    }
}
