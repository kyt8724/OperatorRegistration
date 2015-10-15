using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OperatorRegistration.Repositories;
using OperatorRegistration.Entities;

namespace OperatorRegistration.Utilities
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// Scripted Date : 7 Feb 2012 
    /// Last Modified Date : 9 Feb 2012
    /// Insert data into the database from xml
    /// </summary>

    public class InsertData
    {
        private secdivDataContext db;

        public bool InsertPerson(Trainee trainee)
        {
            // insert person

            try
            {
                db = new secdivDataContext();
                Person person = new Person
                {
                   PID = trainee.PID,
                   LastName = trainee.LastName,
                   HomePhone = trainee.HomePhone,
                   MobilePhone = trainee.MobilePhone,
                   FirstName = trainee.FirstName,
                   MiddleName = trainee.MiddleName,
                   DateOfBirth = trainee.DateOfBirth,
                   Gender = trainee.Gender,
                   OfficePhone = trainee.OfficePhone,
                   Fax = trainee.Fax,
                   Photo = trainee.Photo,
                   EMail = trainee.Email,
                   RankID = trainee.RankID,
                   PersonStatus = "Current",
                   TypeOfPID = trainee.TypeOfPID,
                   RegDate = trainee.RegDate,
                   JobTitle = trainee.JobTitle,
                   PersonRemarks = trainee.PersonRemarks,
                   OFFICECODE = trainee.OfficeCode
                };
                db.Persons.InsertOnSubmit(person);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool InsertPersonDetail(Trainee trainee)
        {
            // insert person details

            try
            {
                db = new secdivDataContext();
                PersonDetail personDetail = new PersonDetail
                {
                    PID = trainee.PID,
                    CountryCode = trainee.Nationality
                };
                db.PersonDetails.InsertOnSubmit(personDetail);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool InsertDbidsOperator(Trainee trainee)
        {
            // insert dbids operator

            try
            {
                db = new secdivDataContext();
                DbidsOperator dbidsOperator = new DbidsOperator
                {
                    PID = trainee.PID,
                    OperatorType = trainee.OperatorType,
                    LastTrainedDate = trainee.LastTrainedDate
                };
                db.DbidsOperators.InsertOnSubmit(dbidsOperator);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool InsertDbidsCertificate(Trainee trainee)
        {
            // insert dbids certificate
            try
            {
                db = new secdivDataContext();
                string remarks = string.Format("{0}. - Update thru trainee registration program", trainee.PersonRemarks);
                DbidsCertificate dbidsCertificate = new DbidsCertificate
                {
                    CertificateRegDate = trainee.LastTrainedDate,
                    CertificateStatus = "Current",
                    OperatorPID = trainee.PID,
                    CertificateRemarks = remarks,
                    CertificateUpdatedDate = trainee.LastTrainedDate,
                    CertificateExpiredDate = trainee.LastTrainedDate.AddYears(1)
                };
                db.DbidsCertificates.InsertOnSubmit(dbidsCertificate);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool InsertTrainingHistory(Trainee trainee)
        {
            // insert training history

            try
            {
                db = new secdivDataContext();
                TrainingHistory trainingHistory = new TrainingHistory
                {
                    TrainedDate = trainee.LastTrainedDate,
                    ClassType = trainee.OperatorType,
                    TraineeLastName = trainee.LastName,
                    TraineeFirstName = trainee.FirstName,
                    TraineeMiddleName = trainee.MiddleName,
                    Trainee = trainee.PID,
                    Trainer = "111111119",
                    TrainedLocation = trainee.Installation,
                    NextTypeofClass = "Refresh",
                    TrainingStatus = "Completed",
                    TrainingResult = "Pass"
                };
                db.TrainingHistories.InsertOnSubmit(trainingHistory);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool InsertUserAccount(Trainee trainee)
        {
            // insert user account

            try
            {
                string userName = string.Format("{0}{1}{2}", trainee.LastName, trainee.FirstName.Substring(0, 1), trainee.PID.Substring(trainee.PID.Length - 4, 4));
                string password = string.Format("{0}{1}", trainee.LastName, trainee.PID.Substring(trainee.PID.Length - 4, 4));
                byte[] passwd = DataHash.GetHashData(password, "SHA1");
                secdivDataContext db = new secdivDataContext();

                UserAccount ua = new UserAccount
                {
                    UserName = userName,
                    Password = passwd,
                    PID = trainee.PID,
                    TypeOfUser = trainee.OperatorType,
                    UAStatus = "Current"
                };

                db.UserAccounts.InsertOnSubmit(ua);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
