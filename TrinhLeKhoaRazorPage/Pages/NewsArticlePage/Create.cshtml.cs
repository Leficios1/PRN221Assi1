using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Model;
using DataAccessObject.Model;
using Services.Services.Interface;
using AutoMapper;
using Services.DTO.Request;

namespace TrinhLeKhoaRazorPage.Pages.NewsArticlePage
{
    public class CreateModel : PageModel
    {
        private readonly DataAccessObject.Model.FunewsManagementDbContext _context;
        private readonly ICategoryServices _categoryServices;
        private readonly ISystemAccountServices _systemAccountServices;
        private readonly INewsArticleServices _newsArticleServices;
        private readonly IMapper _mapper;

        public CreateModel(DataAccessObject.Model.FunewsManagementDbContext context, ICategoryServices categoryServices, ISystemAccountServices systemAccountServices
            , INewsArticleServices newsArticleServices, IMapper mapper)
        {
            _context = context;
            _categoryServices = categoryServices;
            _systemAccountServices = systemAccountServices;
            _mapper = mapper;
            _newsArticleServices = newsArticleServices;
        }

        public async Task<IActionResult> OnGet()
        {
            var userRole = HttpContext.Session.GetString("Roles");
            if (userRole != "Staff")
            {
                return RedirectToPage("/LoginPage");
            }
            ViewData["CategoryId"] = new SelectList(await _categoryServices.getAllAsync(), "CategoryId", "CategoryDesciption");
            ViewData["CreatedById"] = new SelectList(await _systemAccountServices.getAllAsync(), "AccountId", "AccountName");
            return Page();
        }

        [BindProperty]
        public NewsArticle NewsArticle { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var data = _mapper.Map<NewsArticleRequestDTO>(NewsArticle);
            await _newsArticleServices.createNewsArticle(data);
            return RedirectToPage("./Index");
        }
    }
}
