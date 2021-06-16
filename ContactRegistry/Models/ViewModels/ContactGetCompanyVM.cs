using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactRegistry.Models.ViewModels
{
    public class ContactGetCompanyVM
    {
        public List<SelectListItem> Companies { get; set; }
        public int SelectedCompanyId { get; set; }

        public int ContactID { get; set; }
    }
}