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
using Services.DTO.Response;

namespace TrinhLeKhoaRazorPage.Pages.ViewHistoryPage
{
    public class ViewHistoryModel : PageModel
    {
        private readonly INewsArticleServices _newsArticleServices;
        private readonly ISystemAccountServices _systemAccountServices;
        public ViewHistoryModel(INewsArticleServices newsArticleServices, ISystemAccountServices systemAccountServices)
        {
            _newsArticleServices = newsArticleServices;
            _systemAccountServices = systemAccountServices;
        }

        public List<NewsArticleResponseDTO> NewsArticle { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var userRole = HttpContext.Session.GetString("Roles");
            if (userRole == "Staff")
            {
                var email = HttpContext.Session.GetString("UserEmail");
                var data = await _systemAccountServices.getAccountInfoByEmail(email);
                NewsArticle = await _newsArticleServices.getByAccountId(data.AccountId);
                return Page();
            }
            else
            {
                return RedirectToPage("/LoginPage");
            }
        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/LoginPage");
        }
    }
}
