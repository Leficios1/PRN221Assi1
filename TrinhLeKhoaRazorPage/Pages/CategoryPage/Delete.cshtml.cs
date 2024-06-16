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
using AutoMapper;

namespace TrinhLeKhoaRazorPage.Pages.CategoryPage
{
    public class DeleteModel : PageModel
    {
        private readonly ICategoryServices _categoryServices;
        private readonly IMapper _mapper;

        public DeleteModel(ICategoryServices categoryServices, IMapper mapper)
        {
            _categoryServices = categoryServices;
            _mapper = mapper;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        [TempData]
        public string ErrorMessage { get; set; }
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

        public async Task<IActionResult> OnPostAsync(short id)
        {
            var flag = await _categoryServices.deleteCategory(id);
            if (flag)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ErrorMessage = "This Category has been News Article so can not deleted";
                return Page();
                // return RedirectToPage("./Index");

            }
        }
    }
}
