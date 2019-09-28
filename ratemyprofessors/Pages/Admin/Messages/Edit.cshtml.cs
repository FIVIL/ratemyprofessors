using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ratemyprofessors.Models;

namespace ratemyprofessors.Pages.Admin.Messages
{
    public class EditModel : PageModel
    {
        private readonly ratemyprofessors.Models.DataBaseContext _context;

        private readonly IConfiguration _configuration;

        public EditModel(ratemyprofessors.Models.DataBaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [BindProperty]
        public ContactUs ContactUs { get; set; }

        [BindProperty]
        public string Replay { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContactUs = await _context.Contacts.FirstOrDefaultAsync(m => m.ID == id);

            if (ContactUs == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await SendMail();

            return RedirectToPage("./Index");
        }



        private async Task SendMail()
        {
            await Task.Run(() =>
            {
                using (var client = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = _configuration["Email:Email"],
                        Password = _configuration["Email:Password"]
                    };

                    client.Credentials = credential;
                    client.Host = _configuration["Email:Host"];
                    client.Port = int.Parse(_configuration["Email:Port"]);
                    client.EnableSsl = false;
                    using (var emailMessage = new MailMessage())
                    {
                        emailMessage.To.Add(new MailAddress(ContactUs.MailAddress));
                        emailMessage.From = new MailAddress(_configuration["Email:Email"]);
                        emailMessage.Sender = new MailAddress(_configuration["Email:Email"]);
                        emailMessage.Priority = MailPriority.High;
                        emailMessage.Subject = "در پاسخ به درخواست شما برای برقرای ارتباط با ما";
                        emailMessage.Body = "<h3>پیام شما:</h3>" +
                        "<p>" + ContactUs.Text + "</p>" +
                        "<hr /><h3>پاسخ:</h3>" +
                         "<p>" + Replay + "</p>" +
                         "<small>اگر این ایمیل ناخواسته برای شما ارسال شده است، آن را نادیده بگیرید</small>";
                        emailMessage.IsBodyHtml = true;
                        client.Send(emailMessage);
                    }
                }
            });
        }
    }
}
