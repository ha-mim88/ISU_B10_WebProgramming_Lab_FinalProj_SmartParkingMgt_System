using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using SPMS_webapp.Data;
using Microsoft.AspNetCore.Identity;

namespace SPMS_webapp.Pages.Admin.RoleMgt
{
    //[Authorize(Roles = "admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public IdentityRole ApplicationRole { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ApplicationRole.Id = Guid.NewGuid().ToString();
            ApplicationRole.NormalizedName = ApplicationRole.Name.ToUpper();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Roles.Add(ApplicationRole);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
