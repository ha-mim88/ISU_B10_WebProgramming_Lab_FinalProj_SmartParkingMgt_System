using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SPMS_webapp.Data;
using SPMS_webapp.Entity;

namespace SPMS_webapp.Pages.Admin.ParkingMeterMgt
{
    public class DetailsModel : PageModel
    {
        private readonly SPMS_webapp.Data.ApplicationDbContext _context;

        public DetailsModel(SPMS_webapp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IOTEnabledParkingMeter IOTEnabledParkingMeter { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iotenabledparkingmeter = await _context.IOTEnabledParkingMeter.FirstOrDefaultAsync(m => m.Id == id);

            if (iotenabledparkingmeter is not null)
            {
                IOTEnabledParkingMeter = iotenabledparkingmeter;

                return Page();
            }

            return NotFound();
        }
    }
}
