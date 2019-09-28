using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ratemyprofessors.Models
{
    public class Account
    {
        [Key]
        public Guid ID { get; set; }

        [StringLength(20)]
        public string  UserName { get; set; }

        [StringLength(20)]
        public string PassWord { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(20)]
        public string LastName { get; set; }

        [StringLength(60),EmailAddress]
        public string Emain { get; set; }

        [DataType(DataType.Date)]
        public DateTime RegistarationDate { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public bool ISAdmin { get; set; }
    }
}
