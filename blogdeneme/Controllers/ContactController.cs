using blogdeneme.ResourceServices;
using Microsoft.AspNetCore.Mvc;

namespace blogdeneme.Controllers
{
   
    public class ContactController : Controller
    {

        private ContactService contactService;

        public ContactController(ContactService contactService)
        {
            this.contactService = contactService;
        }

        public IActionResult Index()
        {
            ViewBag.Contact = contactService.GetKey("Phone").Value;

            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;

            return View();
        }
    }
}
