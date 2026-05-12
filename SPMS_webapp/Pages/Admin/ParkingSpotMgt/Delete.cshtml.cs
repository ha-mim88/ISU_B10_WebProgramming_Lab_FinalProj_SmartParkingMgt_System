using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SPMS_webapp.Data;
using SPMS_webapp.Entity;

namespace SPMS_webapp.Pages.Admin.ParkingSpotMgt
{
    public class DeleteModel : PageModel
    {
        private readonly SPMS_webapp.Data.ApplicationDbContext _context;

        public DeleteModel(SPMS_webapp.Data.ApplicationDbContext context)
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

            var parkingspot = await _context.ParkingSpot.FirstOrDefaultAsync(m => m.Id == id);

            if (parkingspot is not null)
            {
                ParkingSpot = parkingspot;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingspot = await _context.ParkingSpot.FindAsync(id);
            if (parkingspot != null)
            {
                ParkingSpot = parkingspot;
                _context.ParkingSpot.Remove(ParkingSpot);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
