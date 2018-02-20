using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USI.SOS.Site.API.JobReview.Models
{
    public class CalendarScreenFilterData
    {
        public List<string> Classes { get; set; }
        public List<string> ProductLineNos { get; set; }
        public List<Project> Projects { get; set; }
        public List<Task> Tasks { get; set; }
        public List<Models.TruckType> TruckTypes { get; set; }
    }
}