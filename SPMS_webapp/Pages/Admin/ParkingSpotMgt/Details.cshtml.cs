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
    public class DetailsModel : PageModel
    {
        private readonly SPMS_webapp.Data.ApplicationDbContext _context;

        public DetailsModel(SPMS_webapp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
