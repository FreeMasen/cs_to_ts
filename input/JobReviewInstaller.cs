using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using USI.SOS.DAL;

namespace USI.SOS.Site.API.JobReview.Models
{
    public class JobReviewInstaller
    {
        public Guid? ID { get; set; }
        public int? JobCompleteID { get; set; }
        public string FullName { get; set; }
        public string EmployeeNo { get; set; }
        public decimal? DriveTime { get; set; }
        public decimal? WorkTime { get; set; }
        public decimal? HourlyRate { get; set; }
        public decimal? CertifiedRate { get; set; }
        public decimal? EmployeePay { get; set; }
        public decimal? HourlyPay { get; set; }
        public decimal? PiecePay { get; set; }
        public decimal? CertifiedPay { get; set; }
        public decimal? DrivePay { get; set; }
        public decimal? WorkPercent { get; set; }
        public PayType? PayType { get; set; }
        public bool? PostToJob { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? ReasonCode { get; set; }
        public int InstallerTypeID { get; set; }
        public decimal AdditionalPay { get; set; }
        public bool? Deleted { get; set; }
        public decimal? TotalHours { get; set; }

        public void SubmitForJob(DBAccess conn, JobReviewData job)
        {
            if (this.InstallerTypeID == 0) // Regular installer
            {
                switch (this.PayType)
                {
                    case Models.PayType.PieceRate:
                    case Models.PayType.Hourly:
                        conn.ExecuteProcNonQuery("usp_submitsmarttrucktimejnl2", new SqlParameter[] {
                                    new SqlParameter("@DateWorked", job.WorkStart),
                                    new SqlParameter("@EmployeeNo", this.EmployeeNo),
                                    new SqlParameter("@ProjectNo", job.ProjectNo),
                                    new SqlParameter("@JobNo", job.JobNo),
                                    new SqlParameter("@TaskCode", job.TaskNo),
                                    new SqlParameter("@Hours", this.WorkTime + this.DriveTime),
                                    new SqlParameter("@Earnings", this.EmployeePay),
                                    new SqlParameter("@Travel", Convert.ToByte(0)) {DbType = DbType.Byte, IsNullable = false},
                                    new SqlParameter("@TotalLaborAmount", Convert.ToDecimal(0)) {DbType = DbType.Decimal, IsNullable=false},
                                    new SqlParameter("@TaskComplete", job.JobStatus == "Complete" ? 1 : 0),
                                    new SqlParameter("@ETAJobID", Convert.ToInt32(0)) { DbType = DbType.Int32, IsNullable=false },
                                    new SqlParameter("@ReasonCode", ReasonCode ?? 0),
                                    new SqlParameter("@SOSID", this.ID),
                                    new SqlParameter("@Review", Convert.ToInt32(job.LaborReview)) { DbType = DbType.Int32, IsNullable = false },
                                    new SqlParameter("@JobCompleteID", job.JobCompleteID)
                                });
                        break;
                    case Models.PayType.CertifiedHourly:
                    case Models.PayType.CertifiedPieceRate:
                        conn.ExecuteProcNonQuery("usp_submitsmarttrucktimejnl2", new SqlParameter[] {
                                    new SqlParameter("@DateWorked", job.WorkStart),
                                    new SqlParameter("@EmployeeNo", this.EmployeeNo),
                                    new SqlParameter("@ProjectNo", job.ProjectNo),
                                    new SqlParameter("@JobNo", job.JobNo),
                                    new SqlParameter("@TaskCode", job.TaskNo),
                                    new SqlParameter("@Hours", this.WorkTime),
                                    new SqlParameter("@Earnings", this.EmployeePay),
                                    new SqlParameter("@Travel", Convert.ToByte(0)) {DbType = DbType.Byte, IsNullable = false},
                                    new SqlParameter("@TotalLaborAmount", Convert.ToDecimal(0)) {DbType = DbType.Decimal, IsNullable=false},
                                    new SqlParameter("@TaskComplete", job.JobStatus == "Complete" ? 1 : 0),
                                    new SqlParameter("@ETAJobID", Convert.ToInt32(0)) { DbType = DbType.Int32, IsNullable=false },
                                    new SqlParameter("@ReasonCode", ReasonCode ?? 0),
                                    new SqlParameter("@SOSID", this.ID),
                                    new SqlParameter("@Review", Convert.ToInt32(job.LaborReview)) { DbType = DbType.Int32, IsNullable = false },
                                    new SqlParameter("@JobCompleteID", job.JobCompleteID)
                                });
                        conn.ExecuteProcNonQuery("usp_submitsmarttrucktimejnl2", new SqlParameter[] {
                                    new SqlParameter("@DateWorked", job.WorkStart),
                                    new SqlParameter("@EmployeeNo", this.EmployeeNo),
                                    new SqlParameter("@ProjectNo", job.ProjectNo),
                                    new SqlParameter("@JobNo", job.JobNo),
                                    new SqlParameter("@TaskCode", job.TaskNo),
                                    new SqlParameter("@Hours", this.DriveTime),
                                    new SqlParameter("@Earnings", this.DrivePay),
                                    new SqlParameter("@Travel", Convert.ToByte(1)) { DbType = DbType.Byte, IsNullable=false },
                                    new SqlParameter("@TotalLaborAmount", Convert.ToDecimal(0)) {DbType = DbType.Decimal, IsNullable=false},
                                    new SqlParameter("@TaskComplete", job.JobStatus == "Complete" ? 1 : 0),
                                    new SqlParameter("@ETAJobID", Convert.ToInt32(0)) { DbType = DbType.Int32, IsNullable=false },
                                    new SqlParameter("@ReasonCode", ReasonCode ?? 0),
                                    new SqlParameter("@SOSID", this.ID),
                                    new SqlParameter("@Review", Convert.ToInt32(job.LaborReview)) { DbType = DbType.Int32, IsNullable = false },
                                    new SqlParameter("@JobCompleteID", job.JobCompleteID)
                                });
                        break;
                }
            }
            else // temp installer
            {
                // "SET Context_Info 0x55555" is to suppress the last modified date and user from updating
                conn.ExecuteNonQuery("SET Context_Info 0x55555; update dbo.Job set TempInstallerWorked = 1 where id=@jobid", new SqlParameter[] { new SqlParameter("@jobid", job.JobID) });
            }
        }
    }


    public enum PayType
    {
        PieceRate = 0,
        Hourly = 1,
        CertifiedHourly = 2,
        CertifiedPieceRate = 3
    }

    public class UAttendInstaller
    {
        public string Name { get; set; }
        public decimal? UAHours { get; set; }
        public decimal? JRHours { get; set; }
        public int? JobsWorked { get; set; }
        public List<UAttendJob> Jobs { get; set; }
    }

    public class UAttendJob
    {
        public string JobDesc { get; set; }
        public string Truck { get; set; }
        public decimal? Pay { get; set; }
    }
}