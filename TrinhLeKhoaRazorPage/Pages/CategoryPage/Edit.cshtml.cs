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

namespace TrinhLeKhoaRazorPage.Pages.CategoryPage
{
    public class EditModel : PageModel
    {
        private readonly ICategoryServices _categoryServices;
        private readonly IMapper _mapper;
        public EditModel(ICategoryServices categoryServices, IMapper mapper)
        {
            _categoryServices = categoryServices;
            _mapper = mapper;
        }

        [BindProperty]
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
            Category = category;
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
                var data = _mapper.Map<CategoryUpdateRequestDTO>(Category);
                await _categoryServices.updateCategory(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return RedirectToPage("./Index");
        }
    }
}
