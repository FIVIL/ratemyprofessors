using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ratemyprofessors.Models
{
    public class ProfCourse
    {
        [Key]
        public Guid ID { get; set; }

        public Guid ProfessorID { get; set; }
        public Professor Professor { get; set; }

        public Guid CourseID { get; set; }
        public Course Course { get; set; }
    }
}
