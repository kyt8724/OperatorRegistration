using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace OperatorRegistration.Utilities
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// Scripted Date : 17 Jan 2012 
    /// Convert the string to the Hash
    /// </summary>
    public class DBConnectionCheck
    {
        public static bool CheckConnection()
        {
            SqlConnection conn = ConnectionHelper.GetConnection("secdiv");
            
            try
            {
                conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
