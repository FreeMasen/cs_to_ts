using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USI.SOS.Site.API.JobReview.Models
{
    public class JobReviewMaterialDetail
    {
        public bool Include { get; set; }
        public string ItemNo { get; set; }
        public string ItemDescription { get; set; }
        public string LocationDescription { get; set; }
        public string UnitOfMeasureNo { get; set; }
        public DateTime ScheduleRequestedDate { get; set; }
        public DateTime JobReadyDate { get; set; }
        public DateTime CompleteDate { get; set; }
        public decimal Quantity { get; set; }
        public decimal QtyPerUoM { get; set; }
        public decimal PayRate { get; set; }
        public decimal MiscCost { get; set; }
        public decimal Misclabor { get; set; }
        public decimal MaxHeight { get; set; }
        public string Hash { get; set; }
        public decimal Installed { get; set; }
        public decimal QuantityUsed { get; set; }
        public decimal Remaining { get; set; }
        public decimal EstimatedPay { get; set; }
        public decimal PayToday { get; set; }
        public int TaskSchedulingItemID { get; set; }
    }
}