using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ratemyprofessors.Models
{
    public class ContactUs
    {
        [Key]
        public Guid ID { get; set; }

        public string Text { get; set; }

        [StringLength(60)]
        public string MailAddress { get; set; }
    }
}
