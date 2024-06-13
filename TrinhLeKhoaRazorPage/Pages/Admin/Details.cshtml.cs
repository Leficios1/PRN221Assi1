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

namespace TrinhLeKhoaRazorPage.Pages.Admin_Page
{
    public class DetailsModel : PageModel
    {
        private readonly ISystemAccountServices _systemAccountServices;

        public DetailsModel(ISystemAccountServices systemAccountServices)
        {
            _systemAccountServices = systemAccountServices;
        }

      public SystemAccount SystemAccount { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemaccount = await _systemAccountServices.getAccountInfoById(id);
            if (systemaccount == null)
            {
                return NotFound();
            }
            else 
            {
                SystemAccount = systemaccount;
            }
            return Page();
        }
    }
}
