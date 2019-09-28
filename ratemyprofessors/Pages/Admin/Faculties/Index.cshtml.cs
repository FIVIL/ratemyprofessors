using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ratemyprofessors.Models;

namespace ratemyprofessors.Pages.Admin.Faculties
{
    public class IndexModel : PageModel
    {
        private readonly ratemyprofessors.Models.DataBaseContext _context;

        public IndexModel(ratemyprofessors.Models.DataBaseContext context)
        {
            _context = context;
        }

        public IList<Faculty> Faculty { get;set; }

        public async Task OnGetAsync()
        {
            Faculty = await _context.Faculties
                .Include(f => f.University).ToListAsync();
        }
    }
}
