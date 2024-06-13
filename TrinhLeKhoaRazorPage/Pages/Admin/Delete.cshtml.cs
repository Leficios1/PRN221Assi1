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
using Microsoft.Build.ObjectModelRemoting;

namespace TrinhLeKhoaRazorPage.Pages.Admin_Page
{
    public class DeleteModel : PageModel
    {
        private readonly ISystemAccountServices _systemAccountServices;

        public DeleteModel(ISystemAccountServices systemAccountServices)
        {
            _systemAccountServices = systemAccountServices;
        }

        [BindProperty]
      public SystemAccount SystemAccount { get; set; } = default!;
        [TempData]
        public string ErrorMessage {  get; set; }

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemAccount = await _systemAccountServices.getAccountInfoById(id);

            if (systemAccount == null)
            {
                return NotFound();
            }
            else 
            {
                SystemAccount = systemAccount;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(short id)
        {   
            var flag = await _systemAccountServices.deleteAccount(id);
            if (flag)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ErrorMessage = "This Account have NewsArticle so can not deleted!!!";
                return Page();
               // return RedirectToPage("./Index");

            }
            
        }
    }
}
