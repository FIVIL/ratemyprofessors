using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ratemyprofessors.Models;

namespace ratemyprofessors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public CoursesController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet("facs")]
        public IEnumerable<Faculty> GetFacs()
        {
            return _context.Faculties;
        }

        [HttpGet("ByProf/{id}")]
        public async Task<IActionResult> GetCourseProf([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fac = await _context.Professors
                .AsNoTracking()
                .Include(x => x.ProfCourses)
                    .ThenInclude(y => y.Course)
                .FirstOrDefaultAsync(x => x.ID == id);

            if (fac == null)
            {
                return NotFound();
            }
            return Ok(fac.ProfCourses.Select(x => x.Course).Where(x => x.Approved));
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fac = await _context.Faculties
                .AsNoTracking()
                .Include(x => x.Courses)
                .FirstOrDefaultAsync(x => x.AliasName == id);

            if (fac == null)
            {
                return NotFound();
            }
            return Ok(fac.Courses.Where(x => x.Approved).OrderBy(x=>x.Name));
        }
        [HttpGet]
        public IEnumerable<Course> GetAllCourses()
        {
            return _context.Courses.Where(x => x.Approved);
        }
        // POST: api/Courses
        [HttpPost]
        public async Task<IActionResult> PostCourses([FromBody] Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!Guid.TryParse(course.FacID, out var ffID))
            {
                return BadRequest();
            }
            course.ID = Guid.NewGuid();
            course.Approved = false;
            var Profs = course.Profs.Split(';');
            foreach (var item in Profs)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    if (Guid.TryParse(item, out var ID))
                    {
                        var pf = new ProfCourse
                        {
                            ID = Guid.NewGuid(),
                            CourseID = course.ID,
                            ProfessorID = ID
                        };
                        _context.ProfCourses.Add(pf);
                        _context.Entry(pf).State = EntityState.Added;
                    }
                }
            }
            course.FacultyID = ffID;
            var fac = await _context.Faculties.FindAsync(ffID);
            _context.Courses.Add(course);
            _context.Faculties.Update(fac);
            _context.Entry(fac).State = EntityState.Modified;
            _context.Entry(course).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}