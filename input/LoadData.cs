using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USI.SOS.Site.API.JobReview.Models
{

    public class LoadData
    {
        public string LoadNotes { get; set; }
        public string ReturnNotes { get; set; }
        public List<LoadMaterial> Materials { get; set; }

        public LoadData()
        {
            this.LoadNotes = String.Empty;
            this.ReturnNotes = String.Empty;
            this.Materials = new List<LoadMaterial>();
        }
    }
    public class LoadMaterial
    {
        public string Material { get; set; }
        public decimal Loaded { get; set; }
        public string UoM { get; set; }
        public string MaterialNotes { get; set; }
        public decimal Diff { get; set; }
        public double Used { get; set; }
        public double LeftOnSite { get; set; }
        public decimal Returned { get; set; }
    }
}