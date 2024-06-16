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

namespace TrinhLeKhoaRazorPage.Pages.CategoryPage
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryServices _categoryServices;
        private readonly IMapper _mapper;
        public CreateModel(ICategoryServices categoryServices, IMapper mapper)
        {
            _categoryServices = categoryServices;
            _mapper = mapper;
        }

        public IActionResult OnGet()
        {
            var userRole = HttpContext.Session.GetString("Roles");
            if (userRole != "Staff")
            {
                return RedirectToPage("/LoginPage");
            }
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var data = _mapper.Map<CategoryRequestDTO>(Category);
            await _categoryServices.createCategory(data);
            return RedirectToPage("./Index");
        }
    }
}
