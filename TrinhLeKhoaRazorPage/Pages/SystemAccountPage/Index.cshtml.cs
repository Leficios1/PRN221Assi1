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

namespace TrinhLeKhoaRazorPage.Pages.SystemAccountPage
{
    public class IndexModel : PageModel
    {
        private readonly ISystemAccountServices _systemAccountServices;

        public IndexModel(ISystemAccountServices systemAccountServices)
        {
            _systemAccountServices = systemAccountServices;
        }

        public List<SystemAccountResponseDTO> SystemAccount { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var userRole = HttpContext.Session.GetString("Roles");
            if (userRole != "Admin")
            {
                return RedirectToPage("/LoginPage");
            }
            SystemAccount = await _systemAccountServices.getAllAsync();
            return Page();
        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/LoginPage");
        }
    }
}
