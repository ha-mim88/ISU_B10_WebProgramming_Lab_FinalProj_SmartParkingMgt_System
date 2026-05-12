using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SPMS_webapp.Data;
using SPMS_webapp.Entity;

namespace SPMS_webapp.Pages.Admin.ParkingSpotMgt
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
            ViewData["ParkingMeterId"] = new SelectList(_context.IOTEnabledParkingMeter, "Id", "Code");
            return Page();
        }

        [BindProperty]
        public ParkingSpot ParkingSpot { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["ParkingMeterId"] = new SelectList(_context.IOTEnabledParkingMeter, "Id", "Code");

            if (_context.ParkingSpot.Count(a => a.SpotNumber == ParkingSpot.SpotNumber) > 0)
            {
                    ModelState.AddModelError("ParkingSpot.SpotNumber", "Spot number already exists.");
            }
            // add validation logic for parkingmeterid
            if (_context.ParkingSpot.Count(m => m.ParkingMeterId == ParkingSpot.ParkingMeterId) > 0)
            {
                ModelState.AddModelError("ParkingSpot.ParkingMeterId", "Selected parking meter already assigned.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ParkingSpot.Add(ParkingSpot);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
