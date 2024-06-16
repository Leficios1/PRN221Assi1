using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.DTO.Response;
using Services.Services.Interface;

namespace TrinhLeKhoaRazorPage.Pages.AdminPage
{
    public class ReportModel : PageModel
    {
        private readonly INewsArticleServices _newsArticleServices;

        public ReportModel(INewsArticleServices newsArticleServices)
        {
            _newsArticleServices = newsArticleServices;
        }

        [BindProperty(SupportsGet = true)]
        public DateTime StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; }

        public List<NewsArticleResponseDTO> NewsStatistics { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userRole = HttpContext.Session.GetString("Roles");
            if (userRole != "Admin")
            {
                return RedirectToPage("/LoginPage");
            }
            if (StartDate == default(DateTime) || EndDate == default(DateTime))
            {
                StartDate = DateTime.Today.AddMonths(-1);
                EndDate = DateTime.Today;
            }
            NewsStatistics = await _newsArticleServices.createReport(StartDate, EndDate);
            return Page();
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/LoginPage");
        }
    }
}
