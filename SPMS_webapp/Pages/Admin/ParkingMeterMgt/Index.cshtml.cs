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
    public class IndexModel : PageModel
    {
        private readonly SPMS_webapp.Data.ApplicationDbContext _context;

        public IndexModel(SPMS_webapp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<IOTEnabledParkingMeter> IOTEnabledParkingMeter { get;set; } = default!;

        public async Task OnGetAsync()
        {
            IOTEnabledParkingMeter = await _context.IOTEnabledParkingMeter
                //.Where(a=> a.Status == "Active")
                .ToListAsync();
            // select * from IOTEnabledParkingMeter where Status = 'Active'
        }
    }
}
