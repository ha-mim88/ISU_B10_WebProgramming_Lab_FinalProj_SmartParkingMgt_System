using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPMS_webapp.Entity
{
    public class DriverProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string VehicleNo { get; set; }
        public string DriverLicenseNo { get; set; }
        public string ContactNo { get; set; }


        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public IdentityUser? User { get; set; }
    }
}
