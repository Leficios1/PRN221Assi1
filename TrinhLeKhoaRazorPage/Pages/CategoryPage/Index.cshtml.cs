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

namespace TrinhLeKhoaRazorPage.Pages.CategoryPage
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryServices _categoryServices;

        public IndexModel(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        public List<Category> Category { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var userRole = HttpContext.Session.GetString("Roles");
            if (userRole != "Staff")
            {
                return RedirectToPage("/LoginPage");
            }
            Category = await _categoryServices.getAllAsync();
            return Page();
        }
    }
}
