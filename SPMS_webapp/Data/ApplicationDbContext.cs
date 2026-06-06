using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SPMS_webapp.Entity;

namespace SPMS_webapp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {

        public DbSet<IOTEnabledParkingMeter> IOTEnabledParkingMeter { get; set; }
        public DbSet<ParkingSpot> ParkingSpot { get; set; }

        public DbSet<DriverProfile> DriverProfile { get; set; }
        public DbSet<ParkingHistory> ParkingHistory { get; set; }
        public DbSet<ParkingReserveHistory> ParkingReserveHistory { get; set; }
        public DbSet<PaymentHistory> PaymentHistory { get; set; }

    }
}
