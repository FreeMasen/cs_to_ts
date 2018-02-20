using System;
using System.Data;
using System.Data.SqlClient;
using USI.SOS.DAL;

namespace USI.SOS.Site.API.JobReview.Models
{
    public class JobReviewMaterial
    {
        public Guid? ID { get; set; }
        public int JobCompleteID { get; set; }
        public string LoadItemNo { get; set; }
        public decimal? LeftOnSite { get; set; }
        public decimal? Installed { get; set; }
        public string UnitOfMeasureNo { get; set; }
        public decimal? ExtraLaborPay { get; set; }
        public bool? PostToJob { get; set; }
        public decimal? Estimated { get; set; }
        public decimal? InstalledPercent { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? ReasonCode { get; set; }
        public bool? Deleted { get; set; }
        public decimal? EstimatedCost { get; set; }
        public bool? Editable { get; set; }
        public decimal? PreviouslyInstalled { get; set; }

        public void SubmitForJob(DBAccess conn, JobReviewData job)
        {
            if (this.Installed > 0)
            {
                conn.ExecuteProcNonQuery("usp_submitsmarttruckjobjnl", new SqlParameter[] {
                    new SqlParameter("@PostingDate", job.WorkStart),
                    new SqlParameter("@ProjectNo", job.ProjectNo),
                    new SqlParameter("@JobNo", job.JobNo),
                    new SqlParameter("@TaskCode", job.TaskNo),
                    new SqlParameter("@ItemNo", this.LoadItemNo),
                    new SqlParameter("@Quantity", this.Installed),
                    new SqlParameter("@Location", job.MaterialLocation ?? job.WarehouseNo),
                    new SqlParameter("@UnitOfMeasure", this.UnitOfMeasureNo),
                    new SqlParameter("@ETAJobID", Convert.ToInt32(0)) { DbType = DbType.Int32, IsNullable=false },
                    new SqlParameter("@TaskComplete", job.JobStatus == "Complete" ? 1 : 0),
                    new SqlParameter("@SOSID", this.ID),
                    new SqlParameter("@Review", Convert.ToInt32(job.MaterialReview)) { DbType = DbType.Int32, IsNullable = false },
                    new SqlParameter("@JobCompleteID", job.JobCompleteID)
                });
            }
        }
    }
}