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
    public class IndexModel : PageModel
    {
        private readonly ratemyprofessors.Models.DataBaseContext _context;

        public IndexModel(ratemyprofessors.Models.DataBaseContext context)
        {
            _context = context;
        }

        public IList<ContactUs> ContactUs { get;set; }

        public async Task OnGetAsync()
        {
            ContactUs = await _context.Contacts.ToListAsync();
        }
    }
}
