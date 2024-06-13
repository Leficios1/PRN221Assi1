﻿using System;
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

namespace TrinhLeKhoaRazorPage.Pages.NewsArticlePage
{
    public class IndexModel : PageModel
    {   
        private readonly INewsArticleServices _newsArticleServices;

        public IndexModel(INewsArticleServices newsArticleServices)
        {
            _newsArticleServices = newsArticleServices;
        }

        public   List<NewsArticleResponseDTO> NewsArticle { get;set; } = default!;

        public async Task OnGetAsync()
        {
            NewsArticle = await _newsArticleServices.getAll();
        }
    }
}