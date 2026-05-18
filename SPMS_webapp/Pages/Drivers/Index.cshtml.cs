using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SPMS_webapp.Data;
using SPMS_webapp.Entity;
using System.Security.Claims;

namespace SPMS_webapp.Pages.Drivers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public DriverProfile? MyDriverProfile { get; set; }
        public void OnGet()
        {
            // step1: Check if current logged in user has a driver profile
            var isloggedin = User.Identity.IsAuthenticated;
            if (isloggedin) {
                // retrive the user id of the current logged in user
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // step2: If yes, load the driver profile and display it
                MyDriverProfile = _context.DriverProfile.FirstOrDefault(a=>a.UserId == userId);
            }

        }
    }
}
