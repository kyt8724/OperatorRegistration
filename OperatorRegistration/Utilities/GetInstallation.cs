using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OperatorRegistration.Repositories;

namespace OperatorRegistration.Utilities
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// Scripted Date : 3 Feb 2012
    /// Get the installation data
    /// </summary>

    public class GetInstallation
    {
        public INSTALLATION GetInstallationByIC(string ic)
        {
            secdivDataContext db = new secdivDataContext();
            var installation = db.INSTALLATIONs.Where(i => i.INSTALLATIONCODE.Equals(ic));

            if (installation != null)
                return installation.First();
            return null;
        }

        public List<INSTALLATION> GetAllInstallation()
        {
            secdivDataContext db = new secdivDataContext();
            var installations = db.INSTALLATIONs.OrderBy(i => i.INSTALLATIONNAME);

            if (installations != null)
                return installations.ToList();
            return null;
        }
    }
}
