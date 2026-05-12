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
    public class DeleteModel : PageModel
    {
        private readonly SPMS_webapp.Data.ApplicationDbContext _context;

        public DeleteModel(SPMS_webapp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iotenabledparkingmeter = await _context.IOTEnabledParkingMeter.FindAsync(id);
            if (iotenabledparkingmeter != null)
            {
                IOTEnabledParkingMeter = iotenabledparkingmeter;
                _context.IOTEnabledParkingMeter.Remove(IOTEnabledParkingMeter);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
