using blogdeneme.Data;
using blogdeneme.Helpers;
using blogdeneme.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;

namespace blogdeneme.Controllers
{
    public class BlogController : Controller
    {
        DataContext context;
        public BlogController(DataContext context)
        {
            this.context = context;
        }

        public void DropDown()
        {
            List<SelectListItem> value = (from x in context.Languages.ToList()

                                          select new SelectListItem
                                          {
                                              Value = x.Id.ToString(),
                                              Text = x.LanguageName
                                          }).ToList();
            ViewBag.languageSelectList = value;
          

        }
        public IActionResult Create()
        {
            DropDown();

            return View(new BlogViewModel());

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogViewModel model)
        {
            DropDown();

            // Slug oluşturma
            model.Slug = SeoHelper.GenerateSlug(model.Title);

           
           
            
            var blog = new Blog
            {
                Title = model.Title,
                Content = model.Content,
                Slug = model.Slug,
                LanguageId=model.LanguageId
                
            };

            context.Blogs.Add(blog);
            await context.SaveChangesAsync();


            return RedirectToAction("Index", "Home");
        }



        [Route("Blog/Details/{slug}")]

        public async Task<IActionResult> Details(string slug)
        {
            var cookieValue = Request.Cookies[".AspNetCore.Culture"];
            var lang = cookieValue.Split("|").First().Split("=")[1];


            var blogs = await context.Blogs
                .Where(bl => bl.Slug == slug && bl.Language.LanguageName == lang)
                .FirstOrDefaultAsync();

           
            if (blogs == null)
            {
                
                return NotFound();
            }

            var viewModel = new BlogDetailViewModel
            {
                Id = blogs.Id,
                Title = blogs.Title,
                Content = blogs.Content
            };

            return View(viewModel);
        }


    }
}

