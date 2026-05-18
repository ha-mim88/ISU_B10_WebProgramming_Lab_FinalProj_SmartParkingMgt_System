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

namespace SPMS_webapp.Pages.Admin.ParkingMeterMgt
{
    public class EditModel : PageModel
    {
        private readonly SPMS_webapp.Data.ApplicationDbContext _context;

        public EditModel(SPMS_webapp.Data.ApplicationDbContext context)
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

            var iotenabledparkingmeter =  await _context.IOTEnabledParkingMeter.FirstOrDefaultAsync(m => m.Id == id);
            if (iotenabledparkingmeter == null)
            {
                return NotFound();
            }
            IOTEnabledParkingMeter = iotenabledparkingmeter;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            if (_context.IOTEnabledParkingMeter.Count(a => a.Id != IOTEnabledParkingMeter.Id && a.Code.Trim().ToLower() == IOTEnabledParkingMeter.Code.Trim().ToLower()) > 0)
            {
                ModelState.AddModelError("IOTEnabledParkingMeter.Code", "Code already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Attach(IOTEnabledParkingMeter).State = EntityState.Modified;

            try
            {
                _context.IOTEnabledParkingMeter.Update(IOTEnabledParkingMeter);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IOTEnabledParkingMeterExists(IOTEnabledParkingMeter.Id))
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

        private bool IOTEnabledParkingMeterExists(int id)
        {
            return _context.IOTEnabledParkingMeter.Any(e => e.Id == id);
        }
    }
}
