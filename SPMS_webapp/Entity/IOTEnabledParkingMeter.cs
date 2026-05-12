namespace SPMS_webapp.Entity
{
    public class IOTEnabledParkingMeter
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Status { get; set; } = "Active"; // e.g., "Active", "Inactive", "Maintenance"
    }
}
