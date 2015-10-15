using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OperatorRegistration.Entities
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// SCRIPTED DATE : 18 Jan 2012
    /// Entity - InstallationNameOptions
    /// </summary>
    public class InstallationNameOptions
    {
        public class InstallationNameOption
        {
            public string InstallationName { get; set; }
            public string InstallationCode { get; set; }
            public string Area { get; set; }
            public string City { get; set; }
        }
        public List<InstallationNameOption> InstallationOptions { get; set; }
    }
}
