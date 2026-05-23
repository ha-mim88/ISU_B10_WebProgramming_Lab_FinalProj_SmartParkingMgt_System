using Microsoft.EntityFrameworkCore;
using SPMS_webapp.Data;
using SPMS_webapp.Entity;

namespace SPMS_webapp.Service
{
    public class ParkingSpotBookingService : IParkingSpotBookingService
    {
        private readonly ApplicationDbContext context;
        public ParkingSpotBookingService(ApplicationDbContext _context)
        {
            context = _context;
        }
        public bool BookParkingSpot(int spotId, string userId)
        {
            // step 1: find the parking spot by spotId and see if available
            // step 2: check if userId has already booked a parking spot
            // step 3: if available, book the parking spot for the user and mark it as occupied and add a object in ParkingHistory 

            var parkingSpot = context.ParkingSpot.FirstOrDefault(p => p.Id == spotId && p.IsOccupied == false);
            //var isavailable = context.ParkingSpot.Any(p => p.Id == spotId && p.IsOccupied == false);
            //var isavailable = context.ParkingSpot.Any(p => p.Id == spotId && !p.IsOccupied);
            if (parkingSpot != null)
            {
                // apply step 2
                // get driver profile id first
                var driverProfileId = context.DriverProfile.FirstOrDefault(d => d.UserId == userId).Id;

                // check if driverprofileid has already parked in a parking spot and has not checked out yet
                var activeParking = context.ParkingHistory
                    .Any(p => p.DriverProfileId == driverProfileId && p.IsCheckedOut == false);

                if (activeParking) return false;

                context.ParkingHistory.Add(new ParkingHistory
                {
                    DriverProfileId = driverProfileId,
                    ParkingSpotId = spotId,
                    TotalBill = 100, // we can calculate the bill later based on the parking duration and rate
                    ParkingStart = DateTime.Now
                });
                parkingSpot.IsOccupied = true;
                context.Update(parkingSpot);

                context.SaveChanges();
                return true;

                //var reservedObject = context.ParkingReserveHistory.FirstOrDefault(p => p.DriverProfileId == driverProfileId && p.ParkingSpotId == spotId && p.IsConfirmed && p.ReservationEnd == null);
                //if (reservedObject != null) {
                //    // this means user has reseved a parking spot and the reservation is still active, so we cannot book another one for this user

                //    context.ParkingHistory.Add(new ParkingHistory
                //    {
                //        DriverProfileId = driverProfileId,
                //        ParkingSpotId = reservedObject.ParkingSpotId,
                //        TotalBill = 100, // we can calculate the bill later based on the parking duration and rate
                //        ParkingStart = DateTime.Now
                //    });
                //    parkingSpot.IsOccupied = true;
                //    context.Update(parkingSpot);

                //    context.SaveChanges();
                //    return true;
                //}
                //else
                //{
                //    context.ParkingHistory.Add(new ParkingHistory
                //    {
                //        DriverProfileId = driverProfileId,
                //        ParkingSpotId = spotId,
                //        TotalBill = 100, // we can calculate the bill later based on the parking duration and rate
                //        ParkingStart = DateTime.Now
                //    });
                //    parkingSpot.IsOccupied = true;
                //    context.Update(parkingSpot);

                //    context.SaveChanges();
                //    return true;
                //}
            }
            else return false;

        }

        public List<ParkingSpot> GetAllParkingSpots()
        {
            return context.ParkingSpot.Include(i=>i.IOTEnabledParkingMeter).ToList();
        }

        public bool HasActiveParking(string userId)
        {
            return context.ParkingHistory.Any(p => p.DriverProfile.UserId == userId && p.IsCheckedOut == false);
        }
        public int? GetActiveParkingSpotId(string userId)
        {
            return context.ParkingHistory.FirstOrDefault(p => p.DriverProfile.UserId == userId && p.IsCheckedOut == false)?.ParkingSpotId;
        }
    }
}
