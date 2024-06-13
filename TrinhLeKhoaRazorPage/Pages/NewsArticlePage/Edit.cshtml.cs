using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Model;
using DataAccessObject.Model;
using Services.Services.Interface;
using AutoMapper;
using Services.DTO.Request;

namespace TrinhLeKhoaRazorPage.Pages.NewsArticlePage
{
    public class EditModel : PageModel
    {
        private readonly INewsArticleServices _newsArticleServices;
        private readonly ICategoryServices _categoryServices;
        private readonly ISystemAccountServices _systemAccountServices;
        private readonly IMapper _mapper;
        public EditModel(INewsArticleServices newsArticleServices,
            ICategoryServices categoryServices, ISystemAccountServices systemAccountServices, IMapper mapper)
        {
            _newsArticleServices = newsArticleServices;
            _categoryServices = categoryServices;
            _systemAccountServices = systemAccountServices;
            _mapper = mapper;
        }

        [BindProperty]
        public NewsArticle NewsArticle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsarticle =  await _newsArticleServices.getById(id);
            if (newsarticle == null)
            {
                return NotFound();
            }
            NewsArticle = newsarticle;
           ViewData["CategoryId"] = new SelectList(await _categoryServices.getAllAsync(), "CategoryId", "CategoryDesciption");
           ViewData["CreatedById"] = new SelectList(await _systemAccountServices.getAllAsync(), "AccountId", "AccountName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                var data = _mapper.Map<NewsArticleRequestDTO>(NewsArticle);
                await _newsArticleServices.UpdateNewsArticleAsync(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return RedirectToPage("./Index");
        }
    }
}
