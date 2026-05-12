using System.ComponentModel.DataAnnotations.Schema;

namespace SPMS_webapp.Entity
{
    public class ParkingSpot
    {
        public int Id { get; set; }
        public int SpotNumber { get; set; }
        public bool IsOccupied { get; set; } = false;


        [ForeignKey(nameof(IOTEnabledParkingMeter))]
        public int ParkingMeterId { get; set; }
        public IOTEnabledParkingMeter? IOTEnabledParkingMeter { get; set; }
    }
}
