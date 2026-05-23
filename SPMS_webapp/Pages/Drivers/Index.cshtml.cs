using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SPMS_webapp.Data;
using SPMS_webapp.Entity;
using SPMS_webapp.Service;
using System.Security.Claims;

namespace SPMS_webapp.Pages.Drivers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IParkingSpotBookingService _ps;
        public IndexModel(ApplicationDbContext context, IParkingSpotBookingService ps)
        {
            _context = context;
            _ps = ps;
        }

        public DriverProfile? MyDriverProfile { get; set; }

        [BindProperty]
        public DriverProfile Input { get; set; } = new DriverProfile();

        public void OnGet()
        {
            var isloggedin = User.Identity?.IsAuthenticated == true;
            if (isloggedin)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                MyDriverProfile = _context.DriverProfile.FirstOrDefault(a => a.UserId == userId);

                if (MyDriverProfile != null)
                {
                    Input.Name = MyDriverProfile.Name;
                    Input.DriverLicenseNo = MyDriverProfile.DriverLicenseNo;
                    Input.VehicleNo = MyDriverProfile.VehicleNo;
                    Input.ContactNo = MyDriverProfile.ContactNo;
                }
            }
        }

        public IActionResult OnPost()
        {
            if (User.Identity?.IsAuthenticated != true)
            {
                return Challenge();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Challenge();
            }

            var existingProfile = _context.DriverProfile.FirstOrDefault(a => a.UserId == userId);

            if (existingProfile == null)
            {
                var newProfile = new DriverProfile
                {
                    Name = Input.Name,
                    DriverLicenseNo = Input.DriverLicenseNo,
                    VehicleNo = Input.VehicleNo,
                    ContactNo = Input.ContactNo,
                    UserId = userId
                };

                _context.DriverProfile.Add(newProfile);
            }
            else
            {
                existingProfile.Name = Input.Name;
                existingProfile.DriverLicenseNo = Input.DriverLicenseNo;
                existingProfile.VehicleNo = Input.VehicleNo;
                existingProfile.ContactNo = Input.ContactNo;
            }

            _context.SaveChanges();
            return RedirectToPage();
        }
        public IActionResult OnPostParkNow(int SpotId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var res = _ps.BookParkingSpot(SpotId, userId);
            if (res)
            {
                TempData["SuccessMessage"] = "Parking spot booked successfully!";

            }
            else
            {
                TempData["ErrorMessage"] = "Failed to book parking spot. Please try again.";
            }
            return RedirectToPage("/Drivers/Index");
        }
    }
}
