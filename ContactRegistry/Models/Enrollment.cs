using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactRegistry.Models
{

    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int ContactID { get; set; }
        public int CompanyID { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual Company Company { get; set; }

    }
}