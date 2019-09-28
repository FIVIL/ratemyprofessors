using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ratemyprofessors.Models;

namespace ratemyprofessors.Pages.Admin.Mailes
{
    public class DeleteModel : PageModel
    {
        private readonly ratemyprofessors.Models.DataBaseContext _context;

        public DeleteModel(ratemyprofessors.Models.DataBaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Email Email { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Email = await _context.Emails.FirstOrDefaultAsync(m => m.ID == id);

            if (Email == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Email = await _context.Emails.FindAsync(id);

            if (Email != null)
            {
                _context.Emails.Remove(Email);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
