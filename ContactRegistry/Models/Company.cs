using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactRegistry.Models
{
    public class Company
    {
        //Primary key 
        public int ID { get; set; }
        [Required(ErrorMessage = "Company name is required")]
        public string CompanyName { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}