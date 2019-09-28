using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ratemyprofessors.Models
{
    public class University
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte ID { get; set; }

        [StringLength(40)]
        public string Name { get; set; }

        public ICollection<Faculty> Faculties { get; set; }
    }
}
