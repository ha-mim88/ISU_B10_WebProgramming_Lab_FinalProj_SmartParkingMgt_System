using System.ComponentModel.DataAnnotations.Schema;

namespace SPMS_webapp.Entity
{
    public class ParkingHistory
    {
        public int Id { get; set; }

        public DateTime ParkingStart { get; set; } = DateTime.Now;
        public DateTime? ParkingEnd { get; set; }

        public decimal TotalBill { get; set; } = 0;

        public bool IsCheckedOut { get; set; }

        // Foreign key to DriverProfile

        [ForeignKey(nameof(DriverProfile))]
        public int DriverProfileId { get; set; }
        public DriverProfile? DriverProfile { get; set; }


        // Foreign key to ParkingSpot

        [ForeignKey(nameof(ParkingSpot))]
        public int ParkingSpotId { get; set; }
        public ParkingSpot? ParkingSpot { get; set; }


        // Foreign key to PaymentHistory

        [ForeignKey(nameof(PaymentHistory))]
        public int? PaymentHistoryId { get; set; }
        public PaymentHistory? PaymentHistory { get; set; }
    }
}
