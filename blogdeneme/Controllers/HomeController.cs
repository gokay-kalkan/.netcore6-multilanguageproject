using blogdeneme.Data;
using blogdeneme.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using blogdeneme.ResourceServices;

namespace blogdeneme.Controllers
{
    public class HomeController : Controller
    {

        DataContext context;

      
        private ContactService contactService;
        private PrivacyService privacyService;

        private IStringLocalizer<HomeController> localizer;
        public HomeController(DataContext context, ContactService contactService, PrivacyService privacyService, IStringLocalizer<HomeController> localizer)
        {
            this.context = context;

           
            this.privacyService = privacyService;
            this.contactService = contactService;
            this.localizer = localizer;
        }
        public IActionResult Index()
        {


            if (Request.Cookies.ContainsKey(".AspNetCore.Culture"))
            {
                var cookieValue = Request.Cookies[".AspNetCore.Culture"];
                var lang = cookieValue.Split("|").First().Split("=")[1];

                ViewBag.language = lang;

                var defaultBlogs = context.Blogs.Where(x=>x.Language.LanguageName==lang).ToList();
    
                return View(defaultBlogs);
            }
            else
            {
                // Çerez yok, varsayılan dildeki blogları listeleme

                var defaultBlogs = context.Blogs.Where(x=>x.Language.LanguageName=="tr-TR").ToList();
                return View(defaultBlogs);
            }

        }

        public IActionResult Privacy()
        {

            ViewBag.Paragraf = privacyService.GetKey("Paragraf").Value;

            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;

            return View();
        }

        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                });

            HttpContext.Session.SetString("culture", culture);
            return Redirect(Request.Headers["Referer"].ToString());

        }
        
    }
}