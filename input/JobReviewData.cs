using System;
using System.Collections.Generic;

namespace USI.SOS.Site.API.JobReview.Models
{
    public class JobReviewData
    {
        public Guid JobReviewID { get; set; }
        public int JobCompleteID { get; set; }
        public string TruckNumber { get; set; }
        public string TaskNo { get; set; }
        public string JobNo { get; set; }
        public DateTime? Received { get; set; }
        public string CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public string JobStatus { get; set; }
        public string JobNotCompleteReason { get; set; }
        public string ProjectNo { get; set; }
        public DateTime? WorkStart { get; set; }
        public string AdditionalNotes { get; set; }
        public decimal? BudgetedPay { get; set; }
        public decimal? BudgetedTravel { get; set; }
        public PayType? PayType { get; set; }
        public bool? CertifiedPayroll { get; set; }
        public decimal? Rate { get; set; }
        public decimal? AdditionalPay { get; set; }
        public string AdditionalPayNotes { get; set; }
        public string MaterialLocation { get; set; }
        public bool? Chargeable { get; set; }
        public bool? OrigJobComplete { get; set; }
        public bool? Repair { get; set; }
        public string RepairNotes { get; set; }
        public string RepairNo { get; set; }
        public string OriginalTaskGroupDescription { get; set; }
        public int? DaysWorked { get; set; }
        public decimal? Paid { get; set; }
        public string Class { get; set; }
        public string JobDescription { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string ProductionManagerNote { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool? MultiJob { get; set; }
        public string SubmittedBy { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public bool FromBatch { get; set; }
        public string TruckProjectNo { get; set; }
        public string SalespersonNo { get; set; }
        public bool TempInstallerWorked { get; set; }
        public decimal EstLabor { get; set; }
        public decimal AdjustedLabor { get; set; }
        public int AdjustedLaborDirection { get; set; }
        public decimal? EstimatedTime { get; set; }
        public decimal? ActualTime { get; set; }
        public decimal? ActualPay { get; set; }
        public string WarehouseNo { get; set; }
        public int? AssignmentOrder { get; set; }
        public DateTime? ReceivedLocal { get; set; }
        public bool RequiresAttention { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public string ReviewedBy { get; set; }
        public bool PreviousVersion { get; set; }
        public int ActivityID { get; set; }
        public Guid TaskSchedulingID { get; set; }
        public Guid JobID { get; set; }
        public string SalesEmail { get; set; }
        public bool IsTablet { get; set; }
        public string JCCreatedBy { get; set; }
        public Guid TruckLoadID { get; set; }
        public bool LaborReview { get; set; }
        public bool MaterialReview { get; set; }
        public bool NoReview { get; set; }
        public char? PayOption { get; set; }
        public char? TravelPayOption { get; set; }
        public decimal? OtherPayOptionValue { get; set; }
        public decimal? OtherTravelPayOptionValue { get; set; }
        public string ProductLineNo { get; set; }
        public List<JobReviewInstaller> Installers { get; set; }
        public List<JobReviewMaterial> Materials { get; set; }
        public List<string> PreviousProductionManagerNotes { get; set; }
        public bool AllowsAdjustedLabor { get; set; }
        public bool? OtherTruck { get; set; }
        public string FlaggedBy { get; set; }
        public DateTime? FlaggedDate { get; set; }
        public List<string> WorkOrderNotes { get; set; }
    }


    public class PostJobReview
    {
        public PayType? PayType { get; set; }
        public decimal? AdditionalPay { get; set; }
        public string AdditionalPayNotes { get; set; }
        public string MaterialLocation { get; set; }
        public string ProductionManagerNote { get; set; }
        public string JobStatus { get; set; }
        public string JobNotCompleteReason { get; set; }
        public bool? Submitting { get; set; }
        public bool LaborReview { get; set; }
        public bool MaterialReview { get; set; }
        public bool NoReview { get; set; }
        public char PayOption { get; set; }
        public char TravelPayOption { get; set; }
        public decimal? OtherPayOptionValue { get; set; }
        public decimal? OtherTravelPayOptionValue { get; set; }

        public PostJobReview() { }

        public PostJobReview(JobReviewData data)
        {
            this.PayType = data.PayType;
            this.AdditionalPay = data.AdditionalPay;
            this.AdditionalPayNotes = data.AdditionalPayNotes;
            this.MaterialLocation = data.MaterialLocation;
            this.ProductionManagerNote = data.ProductionManagerNote;
            this.JobStatus = data.JobStatus;
            this.JobNotCompleteReason = data.JobNotCompleteReason;
            this.LaborReview = data.LaborReview;
            this.MaterialReview = data.MaterialReview;
            this.NoReview = data.NoReview;
            this.PayOption = data.PayOption ?? ((data.Repair ?? false) ? 'b' : 'r');
            this.TravelPayOption = data.TravelPayOption ?? 'b';
            this.OtherPayOptionValue = data.OtherPayOptionValue;
            this.OtherTravelPayOptionValue = data.OtherTravelPayOptionValue;
        }
    }


    public class Pay
    {
        private Pay(char value) { Value = value; }

        public char Value { get; set; }
        
        public static Pay BudgetedPay { get { return new Pay('b'); } }
        public static Pay PartialPay { get { return new Pay('p'); } }
        public static Pay RemainingPay { get { return new Pay('r'); } }
        public static Pay TabletPay { get { return new Pay('t'); } }
        public static Pay EstimatedPay { get { return new Pay('e'); } }
        public static Pay AdjustedPay { get { return new Pay('a'); } }
        public static Pay Other { get { return new Pay('o'); } }
    }


    public class TravelPay
    {
        private TravelPay(char value) { Value = value; }

        public char Value { get; set; }

        public static TravelPay BudgetedTravelPay { get { return new TravelPay('b'); } }
        public static TravelPay OtherTravelPay { get { return new TravelPay('o'); } }
    }


    public class PMNote
    {
        public string ProductionManagerNote { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class WONote
    {
        public int Sequence { get; set; }
        public string Description { get; set; }
        public string WorkOrderNote { get; set; }
    }

}