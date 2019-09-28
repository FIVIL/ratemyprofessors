using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ratemyprofessors.Models
{
    public class Course
    {
        [Key]
        public Guid ID { get; set; }

        [StringLength(40)]
        public string Name { get; set; }

        [StringLength(10)]
        public string AliasNames { get; set; }

        [NotMapped]
        public string FacID { get; set; }

        public Guid? FacultyID { get; set; }

        [JsonIgnore]
        public Faculty Faculty { get; set; }

        [JsonIgnore]
        public ICollection<ProfCourse> ProfCourses { get; set; }

        [NotMapped]
        public string Profs { get; set; }

        public bool Approved { get; set; }
    }
}
