using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SPMS_webapp.Data;
using SPMS_webapp.Entity;

namespace SPMS_webapp.Pages.Admin.ParkingMeterMgt
{
    public class CreateModel : PageModel
    {
        private readonly SPMS_webapp.Data.ApplicationDbContext _context;

        public CreateModel(SPMS_webapp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public IOTEnabledParkingMeter IOTEnabledParkingMeter { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.IOTEnabledParkingMeter.Count(a=>a.Code.Trim().ToLower() == IOTEnabledParkingMeter.Code.Trim().ToLower()) > 0)
            {
                ModelState.AddModelError("IOTEnabledParkingMeter.Code", "Code already exists.");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.IOTEnabledParkingMeter.Add(IOTEnabledParkingMeter);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
