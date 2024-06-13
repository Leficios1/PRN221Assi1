using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Model;
using DataAccessObject.Model;
using Services.Services.Interface;
using AutoMapper;
using Services.DTO.Request;

namespace TrinhLeKhoaRazorPage.Pages.StaffProfile
{
    public class EditModel : PageModel
    {
        private readonly ISystemAccountServices _systemAccountServices;
        private readonly IMapper _mapper;

        public EditModel(ISystemAccountServices systemAccountServices, IMapper mapper)
        {
            _systemAccountServices = systemAccountServices;
            _mapper = mapper;
        }

        [BindProperty]
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
            SystemAccount = systemaccount;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                var data = _mapper.Map<SystemAccountRequestDTO>(SystemAccount);
                await _systemAccountServices.updateAccount(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return RedirectToPage("./Index");
        }
    }
}
