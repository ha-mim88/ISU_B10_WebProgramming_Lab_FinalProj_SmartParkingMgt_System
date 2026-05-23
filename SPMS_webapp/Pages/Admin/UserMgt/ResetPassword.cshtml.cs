using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using SPMS_webapp.Data;

namespace SPMS_webapp.Pages.Admin.UserMgt
{
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _db;

        public ResetPasswordModel(UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IdentityUser ApplicationUser { get; set; }
        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string id = null)
        {
            Input = new InputModel();
            ApplicationUser = _db.Users.Find(id);
            Input.Email = ApplicationUser.Email;
            if (id == null || ApplicationUser == null)
            {
                return BadRequest("Invalid User");
            }
            else
            {

                //var code = await _userManager.GeneratePasswordResetTokenAsync(ApplicationUser);
                //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                //Input.Code = code;
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                ModelState.AddModelError("Input.Email", "Invalid user");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, Input.Password);
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToPage("./Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
