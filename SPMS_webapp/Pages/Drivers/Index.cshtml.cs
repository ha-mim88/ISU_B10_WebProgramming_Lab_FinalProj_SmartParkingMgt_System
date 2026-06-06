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
        public IActionResult OnPostCheckOut(int SpotId, int ParkingHistoryId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var parkingHistory = _context.ParkingHistory.FirstOrDefault(a => a.Id == ParkingHistoryId);
            var spot = _context.ParkingSpot.FirstOrDefault(a => a.Id == SpotId);
            // 1. Check if the parking history record exists and belongs to the current user
            // 2. Check if the parking spot exists and is currently occupied
            // 3. If both checks pass, proceed to 
            // 3.1 calculate total bill
            // 3.2 update parking history record with checkout time and total bill
            // 3.3 mark the parking spot as available
            // 3.4 add a payment history record (since it's a simulation, we can assume payment is always successful)
            if(parkingHistory != null && spot != null && spot.IsOccupied == true && parkingHistory.IsCheckedOut == false) 
            {

                // Calculate total bill (example calculation, replace with actual logic)
                var duration = (DateTime.Now - parkingHistory.ParkingStart).TotalMinutes;
                var totalBill = duration * 2; // Example rate of Tk. 2 per minute

                // Update parking history
                parkingHistory.ParkingEnd = DateTime.Now;
                parkingHistory.TotalBill = (decimal) totalBill;
                parkingHistory.IsCheckedOut = true;

                // Mark parking spot as available
                spot.IsOccupied = false;

                // Add payment history (example, replace with actual payment processing)
                var paymentHistory = new PaymentHistory
                {
                    UserId = userId,
                    SpotId = SpotId,
                    Method = "Cash",
                    Amount = (decimal)totalBill, IsSuccess = true,
                    Timestamp = DateTime.Now,
                    Remark = $""
                };
                _context.PaymentHistory.Add(paymentHistory);

                _context.SaveChanges();
                ViewData["SuccessMessage"] = "Checked out successfully!";
            } else
            {
                // this means either the parking history record doesn't exist, the spot doesn't exist, the spot is not occupied, or the parking history record is already checked out
                ViewData["ErrorMessage"] = "Failed to check out. Please try again.";
            }


            return RedirectToPage("/Drivers/Index");
        }
    }
}
