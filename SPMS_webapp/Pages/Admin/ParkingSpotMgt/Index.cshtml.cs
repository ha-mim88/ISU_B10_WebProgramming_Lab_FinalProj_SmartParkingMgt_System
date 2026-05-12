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
    public class IndexModel : PageModel
    {
        private readonly SPMS_webapp.Data.ApplicationDbContext _context;

        public IndexModel(SPMS_webapp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ParkingSpot> ParkingSpot { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ParkingSpot = await _context.ParkingSpot
                .Include(p => p.IOTEnabledParkingMeter).ToListAsync();
        }
    }
}
