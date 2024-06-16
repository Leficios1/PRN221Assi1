using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrinhLeKhoaRazorPage.Pages.AdminPage
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            var userRole = HttpContext.Session.GetString("Roles");
            if (userRole != "Admin")
            {
                return RedirectToPage("/LoginPage");
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
