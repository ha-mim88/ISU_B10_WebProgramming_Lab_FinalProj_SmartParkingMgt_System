using SPMS_webapp.Entity;

namespace SPMS_webapp.Service
{
    public interface IParkingSpotBookingService
    {
        List<ParkingSpot> GetAllParkingSpots();
        bool BookParkingSpot(int spotId, string userId);
        bool HasActiveParking(string userId);
        int? GetActiveParkingSpotId(string userId);
        ParkingHistory? GetActiveParkingHistory(string userId);
        List<ParkingHistory> GetActiveParkingHistories(string userId);
        List<ParkingHistory> GetAllActiveParkings();
        List<ParkingHistory> GetParkingHistoriesByDays(int days);
    }
}
