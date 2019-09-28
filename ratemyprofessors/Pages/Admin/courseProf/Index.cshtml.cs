using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ratemyprofessors.Models;

namespace ratemyprofessors.Pages.Admin.courseProf
{
    public class IndexModel : PageModel
    {
        private readonly ratemyprofessors.Models.DataBaseContext _context;

        public IndexModel(ratemyprofessors.Models.DataBaseContext context)
        {
            _context = context;
        }

        public IList<ProfCourse> ProfCourse { get;set; }

        public async Task OnGetAsync()
        {
            ProfCourse = await _context.ProfCourses
                .Include(p => p.Course)
                .Include(p => p.Professor).OrderBy(x=>x.CourseID).ToListAsync();
        }
        public async Task OnPostSortProfAsync()
        {
            ProfCourse = await _context.ProfCourses
               .Include(p => p.Course)
               .Include(p => p.Professor).OrderBy(x => x.ProfessorID).ToListAsync();
        }
        public async Task OnPostSortCourseAsync()
        {
            ProfCourse = await _context.ProfCourses
                .Include(p => p.Course)
                .Include(p => p.Professor).OrderBy(x => x.CourseID).ToListAsync();
        }
    }
}
