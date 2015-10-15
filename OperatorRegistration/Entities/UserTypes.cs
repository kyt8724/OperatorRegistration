using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OperatorRegistration.Entities
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// SCRIPTED DATE : 18 Jan 2012
    /// Entity - UserTypes
    /// </summary>
    public class UserTypes
    {
        public class UserTypeOption
        {
            public string Value { get; set; }
            public string UserType { get; set; }
        }
        public List<UserTypeOption> UserTypeOptions { get; set; }
    }
}
