using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USI.SOS.Site.API.JobReview.Models
{
    public class Company
    {
        public Guid CompanyID { get; set; }
        public string NAVName { get; set; }
        public bool PreferCustomerName { get; set; }
    }

    public class Installer
    {
        public Guid InstallerID { get; set; }
        public string EmployeeNo { get; set; }
        public string FullName { get; set; }
        public string ProjectNo { get; set; }
        public decimal HourlyRate { get; set; }
    }

    public class InstallerRate
    {
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string ProjectNo { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal CertifiedRate { get; set; }
        public string UserID { get; set; }
        public bool IsProductionManager { get; set; }
        public int InstallerTypeID { get; set; }
        public string InstallerType { get; set; }
    }

    public class Project
    {
        public Guid ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string Description { get; set; }
    }

    public class Task
    {
        public string TaskNo { get; set; }
        public string Description { get; set; }
    }

    public class Truck
    {
        public Guid TruckID { get; set; }
        public string TruckNumber { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectNo { get; set; }
        public int? TruckTypeID { get; set; }
    }

    public class Warehouse
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ProjectNo { get; set; }
    }

    public class Material
    {
        public string ItemNo { get; set; }
        public string SearchNo { get; set; }
        public string LoadUoM { get; set; }
        public string Description { get; set; }
    }

    public class TruckType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}