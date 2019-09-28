using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ratemyprofessors.Models;

namespace ratemyprofessors.Pages.Admin.Messages
{
    public class DetailsModel : PageModel
    {
        private readonly ratemyprofessors.Models.DataBaseContext _context;

        public DetailsModel(ratemyprofessors.Models.DataBaseContext context)
        {
            _context = context;
        }

        public ContactUs ContactUs { get; set; }

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
    }
}
