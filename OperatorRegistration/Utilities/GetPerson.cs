using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OperatorRegistration.Repositories1;

namespace OperatorRegistration.Utilities
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// Scripted Date : 4 Mar 2013  
    /// Select Persons information from Database
    /// </summary>
    /// 

    public class GetPerson
    {
        public OperatorRegistration.Entities.PersonAndDbidsOperators.Person SearchPersonByPID(string pid)
        {
            try
            {
                secdivLocalDataContext db = new secdivLocalDataContext();
                var persons = db.Persons.Where(p => p.PID.Equals(pid));
                if (persons.Count() > 0)
                {
                    PersonDetail personDetail = new PersonDetail();
                    DbidsOperator dbidsOperator = new DbidsOperator();
                    RankService rankService = new RankService();
                    TrainingHistory trainingHistory = new TrainingHistory();
                    string nationality = string.Empty;
                    string operatoryType = string.Empty;
                    string serviceType = string.Empty;
                    string lastTrainingResult = string.Empty;
                    var personFirst = persons.First();
                    var detailPersons = db.PersonDetails.Where(dp => dp.PID.Equals(pid));
                    var dbidsOperators = db.DbidsOperators.Where(o => o.PID.Equals(pid));
                    var ranks = db.RankServices.Where(r => r.RankID.Equals(personFirst.RankID));
                    var trainingHistories = db.TrainingHistories.Where(h => h.Trainee.Equals(pid)).OrderByDescending(h => h.TrainedDate);
                    if (trainingHistories.Count() > 0)
                    {
                        trainingHistory = trainingHistories.First();
                        lastTrainingResult = trainingHistory.TrainingResult;

                    }
                    else
                        lastTrainingResult = "Failed";


                    if (ranks.Count() > 0)
                    {
                        rankService = ranks.First();
                        serviceType = rankService.ServiceType;
                    }

                    if (detailPersons.Count() > 0)
                    {
                        personDetail = detailPersons.First();
                        nationality = personDetail.CountryCode;
                    }
                    if (dbidsOperators.Count() > 0)
                    {
                        dbidsOperator = dbidsOperators.First();
                        operatoryType = dbidsOperator.OperatorType;
                    }
                    Byte[] photo = null;
                    if (personFirst.Photo != null)
                        photo = personFirst.Photo.ToArray();
                    OperatorRegistration.Entities.PersonAndDbidsOperators.Person person = new Entities.PersonAndDbidsOperators.Person
                    {
                        PID = personFirst.PID,
                        LastName = personFirst.LastName,
                        MobilePhone = personFirst.MobilePhone,
                        FirstName = personFirst.FirstName,
                        MiddleName = personFirst.MiddleName,
                        DateOfBirth = personFirst.DateOfBirth.ToString(),
                        Gender = personFirst.Gender,
                        OfficePhone = personFirst.OfficePhone,
                        Email = personFirst.EMail,
                        TypeOfPID = personFirst.TypeOfPID,
                        isPolicyAgree = "false",
                        JobTitle = personFirst.JobTitle,
                        ServiceType = serviceType,
                        RankID = personFirst.RankID.ToString(),
                        Nationality = nationality,
                        OperatorType = operatoryType,
                        OfficeCode = personFirst.OFFICECODE,
                        PersonRemarks = personFirst.PersonRemarks,
                        Installation = personFirst.OFFICECODE.Substring(0, 2),
                        LastTrainingResult = lastTrainingResult,
                        fromPhoto = photo
                    };
                    return person;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        // Get person by certificate number
        public String SearchPersonByCertificate(string certificate)
        {
            try
            {
                secdivLocalDataContext db = new secdivLocalDataContext();
                DbidsCertificate dbidsCertificate = db.DbidsCertificates.Where(dc => dc.CertificateNo.Equals(certificate)).First();
                var persons = db.Persons.Where(p => p.PID.Equals(dbidsCertificate.OperatorPID)).OrderBy(p => p.LastName).OrderBy(p => p.FirstName);
                if (persons.Count() > 0)
                {
                    var person = persons.First();
                    return person.PID;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
