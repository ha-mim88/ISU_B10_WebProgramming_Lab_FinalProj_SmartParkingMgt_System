using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SPMS_webapp.Entity;

namespace SPMS_webapp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {

        public DbSet<IOTEnabledParkingMeter> IOTEnabledParkingMeter { get; set; }
        public DbSet<ParkingSpot> ParkingSpot { get; set; }
    }
}
