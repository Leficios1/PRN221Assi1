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

namespace TrinhLeKhoaRazorPage.Pages.StaffProfile
{
    public class IndexModel : PageModel
    {
        private readonly ISystemAccountServices _systemAccountService;

        public IndexModel(ISystemAccountServices systemAccountServices)
        {
            _systemAccountService = systemAccountServices;
        }

        public SystemAccount SystemAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            var userRole = HttpContext.Session.GetString("Roles");
            if (userRole != "Staff")
            {
                return RedirectToPage("/LoginPage");
            }
            else if (string.IsNullOrEmpty(email))
            {
                return RedirectToPage("/LoginPage");
            }
            SystemAccount = await _systemAccountService.getAccountInfoByEmail(email);
            if (SystemAccount == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/LoginPage");
        }
    }
}
