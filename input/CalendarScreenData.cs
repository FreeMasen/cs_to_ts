using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USI.SOS.Site.API.JobReview.Models
{
    public class CalendarScreenData
    {
        public int ActivityID { get; set; }
        public DateTime Date { get; set; }
        public Guid? JobReviewID { get; set; }
        public string TruckNumber { get; set; }
        public string TruckProjectNo { get; set; }
        public Guid? TruckID { get; set; }
        public string JobNo { get; set; }
        public string JobProjectNo { get; set; }
        public int JobCompleteID { get; set; }
        public string ProductLine { get; set; }
        public string TaskNo { get; set; }
        public string ClassCode { get; set; }
        public bool Reviewed { get; set; }
        public bool Submitted { get; set; }
        public bool RequiresAttention { get; set; }
        public string Status { get; set; }
        public string AreaColor { get; set; }
        public string JobAddress { get; set; }
        public string JobDescription { get; set; }
        public string PartialDescription { get; set; }
        public Guid TaskSchedulingID { get; set; }
        public string TruckTypeName { get; set; }
        public int Instance { get; set; }
        public DateTime SLAStart { get; set; }
        public string ScheduleNote { get; set; }
    }


    public class CalendarScreenDay
    {
        public DateTime Date { get; set; }

        public List<CalendarScreenDayTruck> Trucks { get; set; }
        public List<CalendarScreenJob> UnallocatedJobs { get; set; }
    }


    public class CalendarScreenDayTruck : CalendarScreenTruck
    {
        public List<CalendarScreenJob> Jobs { get; set; }
        public List<CalendarScreenInstaller> Installers { get; set; }
        public string TruckTypeName { get; set; }

        public CalendarScreenDayTruck() { }

        public CalendarScreenDayTruck(DateTime day, CalendarScreenTruck truck, IEnumerable<CalendarScreenData> jobs, List<CalendarScreenDayInstaller> installers)
        {
            this.TruckID = truck.TruckID;
            this.TruckNumber = truck.TruckNumber;
            this.ProjectNo = truck.ProjectNo;
            this.Jobs = jobs.Select(j => new CalendarScreenJob(j)).ToList();
            this.Installers = ((IEnumerable<CalendarScreenInstaller>)installers.Where(i => i.ScheduleDate == day && i.TruckID == truck.TruckID)).ToList();
            this.TruckTypeName = truck.TruckTypeName;
        }
    }
    public class CalendarScreenTruck
    {
        public Guid TruckID { get; set; }
        public string ProjectNo { get; set; }
        public string TruckNumber { get; set; }
        public string TruckTypeName { get; set; }
    }


    public class CalendarScreenJob
    {
        public int ActivityID { get; set; }
        public Guid? JobReviewID { get; set; }
        public string JobNo { get; set; }
        public string JobProjectNo { get; set; }
        public int JobCompleteID { get; set; }
        public string ProductLine { get; set; }
        public string TaskNo { get; set; }
        public string ClassCode { get; set; }
        public bool Reviewed { get; set; }
        public bool Submitted { get; set; }
        public bool RequiresAttention { get; set; }
        public string Status { get; set; }
        public string AreaColor { get; set; }
        public string JobAddress { get; set; }
        public string JobDescription { get; set; }
        public string PartialDescription { get; set; }
        public Guid TaskSchedulingID { get; set; }
        public int Instance { get; set; }
        public DateTime SLAStart { get; set; }
        public string ScheduleNote { get; set; }
        public string TruckNumber { get; set; }
        public CalendarScreenJob(CalendarScreenData data)
        {
            ActivityID = data.ActivityID;
            JobReviewID = data.JobReviewID;
            JobNo = data.JobNo;
            JobProjectNo = data.JobProjectNo;
            JobCompleteID = data.JobCompleteID;
            ProductLine = data.ProductLine;
            TaskNo = data.TaskNo;
            ClassCode = data.ClassCode;
            Reviewed = data.Reviewed;
            Submitted = data.Submitted;
            RequiresAttention = data.RequiresAttention;
            AreaColor = data.AreaColor;
            JobAddress = data.JobAddress;
            JobDescription = data.JobDescription;
            PartialDescription = data.PartialDescription;
            TaskSchedulingID = data.TaskSchedulingID;
            Status = data.Status;
            Instance = data.Instance;
            SLAStart = data.SLAStart;
            ScheduleNote = data.ScheduleNote;
            TruckNumber = data.TruckNumber;
        }
        public CalendarScreenJob() { }
    }

    public class CalendarScreenDayInstaller : CalendarScreenInstaller
    {
        public DateTime ScheduleDate { get; set; }
        public Guid TruckID { get; set; }
    }


    public class CalendarScreenInstaller
    {
        public string EmployeeNo { get; set; }
        public string FullName { get; set; }
    }
}