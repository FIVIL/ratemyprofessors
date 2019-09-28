using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ratemyprofessors.Models
{
    public class Faculty
    {
        [Key]
        public Guid ID { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(20)]
        public string AliasName { get; set; }

        public byte UniversityID { get; set; }
        public University University { get; set; }

        public ICollection<Course> Courses { get; set; }

        public ICollection<ProfFac> ProfFacs { get; set; }
    }
}
