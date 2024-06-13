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
    public class DetailsModel : PageModel
    {
        private readonly INewsArticleServices _newsArticleServices;

        public DetailsModel(INewsArticleServices newsArticleServices)
        {
            _newsArticleServices = newsArticleServices; 
        }

      public NewsArticle NewsArticle { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(string id)
        {
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
    }
}
