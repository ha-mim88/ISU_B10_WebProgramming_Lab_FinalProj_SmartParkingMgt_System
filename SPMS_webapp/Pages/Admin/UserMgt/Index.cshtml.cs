using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SPMS_webapp.Data;

namespace SPMS_webapp.Pages.Admin.UserMgt
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<IndexModel> _logger;
        //private readonly IMailService _emailSender;
        public IndexModel(ApplicationDbContext db, UserManager<IdentityUser> userManager, ILogger<IndexModel> logger/*, IMailService emailSender*/)
        {
            _db = db;
            _userManager = userManager;
            _logger = logger;
            //_emailSender = emailSender;

        }
        public List<IdentityUser> AppUserList { get; set; }
        public void OnGet()
        {
            AppUserList = _db.Users.ToList();
        }
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string id, bool val, string cmd, string role)
        {
            var user = _db.Users.Find(id);
            if(cmd == "remove")
            {
                try
                {
                    await _userManager.RemoveFromRoleAsync(user,role);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

            } else
            {
                //user.IsSuspended = val;
                try
                {
                    _db.Update(user);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostResendConfirmationEmailAsync(string id)
        {
            var user = _db.Users.Find(id);
            //var about = _db.AboutUs.FirstOrDefault();
            if (user != null && user.EmailConfirmed == false)
            {

                _logger.LogInformation("User Email confirmation email resend");

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = Url.Content("~/") },
                    protocol: Request.Scheme);
                try
                {
                    //await _emailSender.SendEmailAsync(new ViewModels.MailRequest
                    //{
                    //    ToEmail = user.Email,
                    //    Subject = "Confirm your email",
                    //    Body = $"Welcome to {about.FullName},<br /> Thanks for your registration <br /><br />Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>. <br /> <br /> <br /> {about.ShortName} <br />"
                    //});
                }
                catch (Exception ex)
                {

                }

            }
            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnPostManualEmailConfirmationAsync(string id)
        {
            var user = _db.Users.Find(id);
            if (user != null && user.EmailConfirmed == false)
            {
                user.EmailConfirmed = true;
                _db.Users.Update(user);
                _db.SaveChanges();
            }
            return RedirectToPage("./Index");
        }
    }
}
