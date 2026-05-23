using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SPMS_webapp.Data;
using Microsoft.AspNetCore.Identity;

namespace SPMS_webapp.Pages.Admin.RoleMgt
{
    //[Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<IdentityRole> ApplicationRole { get;set; }

        public async Task OnGetAsync()
        {
            ApplicationRole = await _context.Roles.ToListAsync();
        }
    }
}
