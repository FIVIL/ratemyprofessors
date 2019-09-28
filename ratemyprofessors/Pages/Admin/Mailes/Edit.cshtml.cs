using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ratemyprofessors.Models;

namespace ratemyprofessors.Pages.Admin.Mailes
{
    public class EditModel : PageModel
    {
        private readonly ratemyprofessors.Models.DataBaseContext _context;

        public EditModel(ratemyprofessors.Models.DataBaseContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Email).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmailExists(Email.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EmailExists(Guid id)
        {
            return _context.Emails.Any(e => e.ID == id);
        }
    }
}
