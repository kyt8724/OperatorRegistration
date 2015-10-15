using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OperatorRegistration.Entities;
using OperatorRegistration.Repositories;

namespace OperatorRegistration.Utilities
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// Scripted Date : 7 Feb 2012  
    /// Last Modified Date : 9 Feb 2012
    /// Update data into the database from xml
    /// </summary>

    public class UpdateData
    {
        public bool UpdatePerson(Trainee trainee)
        {
            // update person
            try
            {
                secdivDataContext db = new secdivDataContext();
                var persons = db.Persons.Where(p => p.PID.Equals(trainee.PID));

                if (persons.Count() > 0)
                {
                    Person person = persons.First();
                    person.LastName = trainee.LastName;
                    person.FirstName = trainee.FirstName;
                    person.MiddleName = trainee.MiddleName;
                    person.TypeOfPID = trainee.TypeOfPID;
                    person.EMail = trainee.Email;
                    person.JobTitle = trainee.JobTitle;
                    person.OFFICECODE = trainee.OfficeCode;
                    person.HomePhone = trainee.HomePhone;
                    person.MobilePhone = trainee.MobilePhone;
                    person.Gender = trainee.Gender;
                    person.DateOfBirth = trainee.DateOfBirth;
                    person.PersonStatus = "Current";
                    person.OfficePhone = trainee.OfficePhone;
                    person.Fax = trainee.Fax;
                    person.Photo = trainee.Photo;
                    person.RankID = trainee.RankID;

                    db.SubmitChanges();
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdatePersonDetail(Trainee trainee)
        {
            // update person details
            try
            {
                secdivDataContext db = new secdivDataContext();
                var personDetails = db.PersonDetails.Where(pd => pd.PID.Equals(trainee.PID));

                if (personDetails.Count() > 0)
                {
                    PersonDetail detailPerson = personDetails.First();
                    detailPerson.CountryCode = trainee.Nationality;

                    db.SubmitChanges();
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateDbidsOperator(Trainee trainee)
        {
            // update dbids operator
            try
            {
                secdivDataContext db = new secdivDataContext();
                var dbidsOperators = db.DbidsOperators.Where(d => d.PID.Equals(trainee.PID));

                if (dbidsOperators.Count() > 0)
                {
                    DbidsOperator dbidsOperator = dbidsOperators.First();
                    dbidsOperator.OperatorType = trainee.OperatorType;
                    dbidsOperator.LastTrainedDate = trainee.LastTrainedDate;

                    db.SubmitChanges();
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateDbidsCertificate(Trainee trainee)
        {
            try
            {
                secdivDataContext db = new secdivDataContext();
                var dbidsCertificates = db.DbidsCertificates.Where(d => d.OperatorPID.Equals(trainee.PID));
                string remarks = string.Format("{0}. - Update thru trainee registration program", trainee.PersonRemarks);

                if (dbidsCertificates.Count() > 0)
                {
                    DbidsCertificate dbidsCertificate = dbidsCertificates.First();
                    dbidsCertificate.CertificateStatus = "Current";
                    dbidsCertificate.CertificateRemarks = remarks;
                    dbidsCertificate.CertificateUpdatedDate = trainee.LastTrainedDate;
                    dbidsCertificate.CertificateExpiredDate = trainee.LastTrainedDate.AddYears(1);

                    db.SubmitChanges();
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateUserAccount(Trainee trainee)
        {
            // update user account
            try
            {
                string userName = string.Format("{0}{1}{2}", trainee.LastName, trainee.FirstName.Substring(0, 1), trainee.PID.Substring(trainee.PID.Length - 4, 4));
                string password = string.Format("{0}{1}", trainee.LastName, trainee.PID.Substring(trainee.PID.Length - 4, 4));
                byte[] passwd = DataHash.GetHashData(password, "SHA1");

                secdivDataContext db = new secdivDataContext();
                var userAccounts = db.UserAccounts.Where(u => u.PID.Equals(trainee.PID));

                if (userAccounts.Count() > 0)
                {
                    UserAccount userAccount = userAccounts.First();
                    if (userAccount.UserName == userName)
                    {
                        userAccount.Password = passwd;
                        userAccount.PID = trainee.PID;
                        userAccount.TypeOfUser = trainee.OperatorType;
                        userAccount.UAStatus = "Current";

                        db.SubmitChanges();
                        return true;
                    }
                    else
                    {
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
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
