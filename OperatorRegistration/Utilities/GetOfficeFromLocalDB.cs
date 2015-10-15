using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OperatorRegistration.Repositories1;

namespace OperatorRegistration.Utilities
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// Scripted Date : 3 Feb 2012
    /// Get the office data
    /// </summary>
    /// 

    public class GetOfficeFromLocalDB
    {
        // Get Offices
        public List<OFFICE> GetOffices(string value)
        {
            try
            {
                secdivLocalDataContext db = new secdivLocalDataContext();
                if (value.Equals("KSG"))
                {
                    var offices = db.OFFICEs.Where(o => o.OfficeName.Contains("C&S")).OrderBy(o => o.OfficeName);
                    if (offices.Count() > 0)
                    {
                        return offices.ToList();
                    }
                    return null;
                }
                else
                {
                    var offices = db.OFFICEs.Where(o => !o.OfficeName.Contains("C&S")).OrderBy(o => o.OfficeName);
                    if (offices.Count() > 0)
                    {
                        return offices.ToList();
                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        // Get All offices
        public List<OFFICE> GetAllOffices()
        {
            try
            {
                secdivLocalDataContext db = new secdivLocalDataContext();
                var offices = db.OFFICEs.Select(o => o).OrderBy(o => o.OfficeName);
                if (offices.Count() > 0)
                {
                    return offices.ToList();
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
