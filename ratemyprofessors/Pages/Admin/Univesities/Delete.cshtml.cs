using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ratemyprofessors.Models;

namespace ratemyprofessors.Pages.Admin.Univesities
{
    public class DeleteModel : PageModel
    {
        private readonly ratemyprofessors.Models.DataBaseContext _context;

        public DeleteModel(ratemyprofessors.Models.DataBaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public University University { get; set; }

        public async Task<IActionResult> OnGetAsync(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            University = await _context.Universities.FirstOrDefaultAsync(m => m.ID == id);

            if (University == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            University = await _context.Universities.FindAsync(id);

            if (University != null)
            {
                _context.Universities.Remove(University);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
