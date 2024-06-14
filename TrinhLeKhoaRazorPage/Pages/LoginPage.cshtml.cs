using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Services.DTO.Request;
using Services.DTO.Response;
using Services.Services.Interface;

namespace TrinhLeKhoaRazorPage.Pages
{
    public class LoginPageModel : PageModel
    {
        private readonly ILogger<LoginPageModel> _logger;
        private readonly ISystemAccountServices _systemAccountServices;
        private readonly IOptions<AdminAccount> _adminAccount;
        public LoginPageModel(ILogger<LoginPageModel> logger, ISystemAccountServices systemAccountServices, IOptions<AdminAccount> option)
        {
            _logger = logger;
            _systemAccountServices = systemAccountServices;
            _adminAccount = option;
        }

        [BindProperty]
        public LoginRequestDTO Input { get; set; }

        public string ReturnURL { get; set; }
        public void OnGet(string returnURL = null)
        {
            ReturnURL = returnURL;
        }

        public async Task<IActionResult> OnPost()
        {
            var email = Input.Email;
            var password = Input.Password;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var flag = await _systemAccountServices.Login(email, password);
            if (flag)
            {
                HttpContext.Session.SetString("UserEmail", email);
                return RedirectToPage("NewsArticlePage/Index");
            }else if(_adminAccount.Value.Email.Equals(email) && _adminAccount.Value.Password.Equals(password))
            {
                HttpContext.Session.SetString("UserEmail", email);
                return RedirectToPage("AdminPage/Index");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();

        }
    }
}
