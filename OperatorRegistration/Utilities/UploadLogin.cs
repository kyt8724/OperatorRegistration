using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OperatorRegistration.Repositories;

namespace OperatorRegistration.Utilities
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// Scripted Date : 6 Feb 2012  
    /// Get the login information and return
    /// </summary>
    public class UploadLogin
    {
        public LoginInfo GetLoginInfo(string id, byte[] passwd)
        {
            secdivDataContext db = new secdivDataContext();

            var users = db.UserAccounts.Where(ua => ua.UserName.Equals(id) && ua.Password.Equals(passwd));

            if (users.Count() > 0)
            {
                UserAccount user = users.First();
                var userinfo = db.Persons.Where(p => p.PID.Equals(user.PID));
                if (userinfo.Count() > 0)
                {
                    LoginInfo li;
                    Person person = userinfo.First();
                    if (String.IsNullOrEmpty(person.MiddleName))
                    {
                        li = new LoginInfo
                        {
                            UserName = user.UserName,
                            Name = string.Format("{0}, {1} {2}", person.LastName, person.FirstName, person.MiddleName),
                            UnitName = person.OFFICE.Unit.UnitName,
                            TypeOfUser = user.TypeOfUser,
                            UAStatus = user.UAStatus
                        };
                    }
                    else
                    {
                        li = new LoginInfo
                        {
                            UserName = user.UserName,
                            Name = string.Format("{0}, {1}", person.LastName, person.FirstName),
                            UnitName = person.OFFICE.Unit.UnitName,
                            TypeOfUser = user.TypeOfUser,
                            UAStatus = user.UAStatus
                        };
                    }
                    return li;
                }
            }
            return null;
        }
    }
}
