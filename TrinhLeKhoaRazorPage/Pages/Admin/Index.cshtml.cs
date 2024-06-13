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

namespace TrinhLeKhoaRazorPage.Pages.Admin_Page
{
    public class IndexModel : PageModel
    {
        private readonly ISystemAccountServices _systemAccountServices;

        public IndexModel(ISystemAccountServices systemAccountServices)
        {
            _systemAccountServices = systemAccountServices;
        }

        public List<SystemAccountResponseDTO> SystemAccount { get;set; } = default!;

        public async Task OnGetAsync()
        {
                SystemAccount = await _systemAccountServices.getAllAsync();
        }
    }
}
