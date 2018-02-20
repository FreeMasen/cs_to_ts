using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USI.SOS.Site.API.JobReview.Models
{
    public class User
    {
        public Guid ID { get; set; }
        public Guid DefaultCompanyID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SalespersonNo { get; set; }
        public string NAVUserNo { get; set; }
        public bool Last10Type { get; set; }
        public string ProjectNo { get; set; }
        public List<string> Roles { get; set; }
        public FilterObject[] FilterFriendlyProjectNo { get; set; }
        public List<Company> Companies { get; set; }
    }

    public class FilterObject
    {
        public string label { get; set; }
        public string value { get; set; }
    }
}