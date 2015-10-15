using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OperatorRegistration.Entities
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// SCRIPTED DATE : 18 Jan 2012
    /// Entity - Trainees
    /// </summary>
    public class Trainee
    {
        public string PID { get; set; }
        public string LastName { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string OfficePhone { get; set; }
        public string Fax { get; set; }
        public Byte[] Photo { get; set; }
        public string Email { get; set; }
        public string ServiceType { get; set; }
        public int RankID { get; set; }
        public string OfficeCode { get; set; }
        public string TypeOfPID { get; set; }
        public DateTime RegDate { get; set; }
        public string JobTitle { get; set; }
        public string PersonRemarks { get; set; }
        public string OperatorType { get; set; }
        public DateTime LastTrainedDate { get; set; }
        public string Installation { get; set; }
        public string InstallationName { get; set; }
        public string Nationality { get; set; }

        public List<Trainee> Trainees { get; set; }
    }
}
