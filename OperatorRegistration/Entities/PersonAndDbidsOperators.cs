using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OperatorRegistration.Entities
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// SCRIPTED DATE : 18 Jan 2012
    /// Entity - PersonAndDbidsOperators
    /// </summary>
    public class PersonAndDbidsOperators
    {
        public class Person
        {
            public string PID { get; set; }
            public string LastName { get; set; }
            public string HomePhone { get; set; }
            public string MobilePhone { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string DateOfBirth { get; set; }
            public string Gender { get; set; }
            public string OfficePhone { get; set; }
            public string Fax { get; set; }
            public string Photo { get; set; }
            public string Email { get; set; }
            public string ServiceType { get; set; }
            public string RankID { get; set; }
            public string OfficeCode { get; set; }
            public string TypeOfPID { get; set; }
            public string RegDate { get; set; }
            public string JobTitle { get; set; }
            public string PersonRemarks { get; set; }
            public string OperatorType { get; set; }
            public string LastTrainedDate { get; set; }
            public string Installation { get; set; }
            public string Nationality { get; set; }
            public string isPolicyAgree { get; set; }
            public Byte[] fromPhoto { get; set; }
            public string LastTrainingResult { get; set; }
        }

        public List<Person> Persons { get; set; }
    }
}
