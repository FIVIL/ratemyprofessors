using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ratemyprofessors.Models;

namespace ratemyprofessors.Pages
{
    public class EmailModel : PageModel
    {
        private readonly DataBaseContext _context;
        public EmailModel(DataBaseContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {
            var id = HttpContext.Request.Query["id"];
            if (!Guid.TryParse(id, out var ID))
            {
                Response.Redirect("/Error");
                return;
            }
            var e = await _context.Emails.FindAsync(ID);
            if (e == null)
            {
                Response.Redirect("/Error");
                return;
            }
            if (!e.Verified) e.Verified = true;
            _context.Entry(e).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            Response.Redirect("/");
        }
    }
}