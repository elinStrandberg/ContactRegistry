using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactRegistry.Models
{
    public class Contact
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ContactID { get; set; }
        [Required(ErrorMessage = "Contact name is required")]
        public string ContactName { get; set; }
        public string Title { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}