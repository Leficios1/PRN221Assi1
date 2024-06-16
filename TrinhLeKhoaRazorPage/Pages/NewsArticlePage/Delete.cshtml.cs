using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Model;
using DataAccessObject.Model;
using Services.Services.Interface;

namespace TrinhLeKhoaRazorPage.Pages.NewsArticlePage
{
    public class DeleteModel : PageModel
    {
        private readonly INewsArticleServices _newsArticleServices;

        public DeleteModel(INewsArticleServices newsArticleServices)
        {
            _newsArticleServices = newsArticleServices;
        }

        [BindProperty]
      public NewsArticle NewsArticle { get; set; } = default!;
        //[TempData]
        //public string ErrorMessage {  get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var userRole = HttpContext.Session.GetString("Roles");
            if (userRole != "Staff")
            {
                return RedirectToPage("/LoginPage");
            }

            if (id == null)
            {
                return NotFound();
            }

            var newsarticle = await _newsArticleServices.getById(id);

            if (newsarticle == null)
            {
                return NotFound();
            }
            else 
            {
                NewsArticle = newsarticle;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _newsArticleServices.deleteNewsArticle(id);
            return RedirectToPage("./Index");
        }
    }
}
