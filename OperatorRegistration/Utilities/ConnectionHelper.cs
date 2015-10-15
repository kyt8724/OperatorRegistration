using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace OperatorRegistration.Utilities
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// Scripted Date : 17 Jan 2012  
    /// Database Connection Helper Method
    /// </summary>
    public class ConnectionHelper
    {
        public static string GetConnectionString(string dsn)
        {
            return ConfigurationManager.ConnectionStrings[dsn].ConnectionString;
        }

        public static SqlConnection GetConnection(string dsn)
        {
            return new SqlConnection(GetConnectionString(dsn));
        }
    }
}
