using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ratemyprofessors.Models;

namespace ratemyprofessors.Pages.Admin.comments
{
    public class DetailsModel : PageModel
    {
        private readonly ratemyprofessors.Models.DataBaseContext _context;

        public DetailsModel(ratemyprofessors.Models.DataBaseContext context)
        {
            _context = context;
        }

        public Comment Comment { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Comment = await _context.Comments
                .Include(c => c.Account)
                .Include(c => c.Professor).FirstOrDefaultAsync(m => m.ID == id);

            if (Comment == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
