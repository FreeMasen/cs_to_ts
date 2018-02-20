using System;

namespace USI.SOS.Site.API.JobReview.Models
{
    public class TruckMaterial
    {
        public Guid ID { get; set; }
        public string CalledForItemNo { get; set; }
        public string CalledForDescription { get; set; }
        public decimal? CalledForQty { get; set; }
        public decimal? RemainingQty { get; set; }
        public string CalledForUoM { get; set; }
        public string AssignmentOrder { get; set; }
        public string LoadItemNo { get; set; }
        public string LoadDescription { get; set; }
        public decimal? LoadQty { get; set; }
        public string LoadUoM { get; set; }
        public string ReasonLoadDiff { get; set; }
        public decimal? Diff { get; set; }
    }
}