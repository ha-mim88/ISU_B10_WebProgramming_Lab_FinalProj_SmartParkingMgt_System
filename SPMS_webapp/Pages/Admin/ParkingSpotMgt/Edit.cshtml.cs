using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SPMS_webapp.Data;
using SPMS_webapp.Entity;

namespace SPMS_webapp.Pages.Admin.ParkingSpotMgt
{
    public class EditModel : PageModel
    {
        private readonly SPMS_webapp.Data.ApplicationDbContext _context;

        public EditModel(SPMS_webapp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ParkingSpot ParkingSpot { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingspot =  await _context.ParkingSpot.FirstOrDefaultAsync(m => m.Id == id);
            if (parkingspot == null)
            {
                return NotFound();
            }
            ParkingSpot = parkingspot;
           ViewData["ParkingMeterId"] = new SelectList(_context.IOTEnabledParkingMeter, "Id", "Code");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["ParkingMeterId"] = new SelectList(_context.IOTEnabledParkingMeter, "Id", "Code");

            if (_context.ParkingSpot.Count(a => a.Id != ParkingSpot.Id && a.SpotNumber == ParkingSpot.SpotNumber) > 0)
            {
                ModelState.AddModelError("ParkingSpot.SpotNumber", "Spot number already exists.");
            }
            // add validation logic for parkingmeterid
            if (_context.ParkingSpot.Count(m => m.Id != ParkingSpot.Id && m.ParkingMeterId == ParkingSpot.ParkingMeterId) > 0)
            {
                ModelState.AddModelError("ParkingSpot.ParkingMeterId", "Selected parking meter already assigned.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ParkingSpot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingSpotExists(ParkingSpot.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ParkingSpotExists(int id)
        {
            return _context.ParkingSpot.Any(e => e.Id == id);
        }
    }
}
