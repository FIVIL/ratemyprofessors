using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ratemyprofessors.Models;

namespace ratemyprofessors.Pages.Admin.facProf
{
    public class IndexModel : PageModel
    {
        private readonly ratemyprofessors.Models.DataBaseContext _context;

        public IndexModel(ratemyprofessors.Models.DataBaseContext context)
        {
            _context = context;
        }

        public IList<ProfFac> ProfFac { get;set; }

        public async Task OnGetAsync()
        {
            ProfFac = await _context.ProfFacs
                .Include(p => p.Faculty)
                .Include(p => p.Professor).OrderBy(x=>x.FacultyID).ToListAsync();
        }
        public async Task OnPostSortProfAsync()
        {
            ProfFac = await _context.ProfFacs
                 .Include(p => p.Faculty)
                 .Include(p => p.Professor).OrderBy(x => x.ProfessorID).ToListAsync();
        }
        public async Task OnPostSortFacAsync()
        {
            ProfFac = await _context.ProfFacs
                .Include(p => p.Faculty)
                .Include(p => p.Professor).OrderBy(x => x.FacultyID).ToListAsync();
        }
    }
}
