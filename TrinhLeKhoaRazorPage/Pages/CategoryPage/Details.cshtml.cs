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
    public class DetailsModel : PageModel
    {
        private readonly ICategoryServices _categoryServices;

        public DetailsModel(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

      public Category Category { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(short id)
        {
            var userRole = HttpContext.Session.GetString("Roles");
            if (userRole != "Staff")
            {
                return RedirectToPage("/LoginPage");
            }
            var category = await _categoryServices.getCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            else 
            {
                Category = category;
            }
            return Page();
        }
    }
}
