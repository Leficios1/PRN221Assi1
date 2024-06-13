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
using Services.DTO.Request;

namespace TrinhLeKhoaRazorPage.Pages.Admin_Page
{
    public class CreateModel : PageModel
    {
        private readonly ISystemAccountServices _systemAccountServices;

        public CreateModel(ISystemAccountServices systemAccountServices)
        {
            _systemAccountServices = systemAccountServices;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SystemAccountRequestDTO SystemAccount { get; set; } = default!;
        

        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || SystemAccount == null)
            {
                return Page();
            }

            await _systemAccountServices.createAccount(SystemAccount);
            return RedirectToPage("/Admin/Index");
        }
    }
}
