using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OperatorRegistration.Repositories;

namespace OperatorRegistration.Utilities
{
    public class GetOffice
    {
        /// <summary>
        /// Scripter : YONGTOK KIM
        /// Scripted Date : 3 Feb 2012
        /// Get the office data
        /// </summary>

        //By the Unit Name
        public List<OFFICE> GetOfficesByUnitName(string unitName)
        {
            secdivDataContext db = new secdivDataContext();
            Unit unit = db.Units.Where(u => u.UnitName.StartsWith(unitName)).First();
            var offices = db.OFFICEs.Where(o => o.UIC.Equals(unit.UIC));
            if (offices.Count() > 0)
            {
                return offices.ToList();
            }
            return null;
        }

        //By the Unit Identification Code(UIC)
        public List<OFFICE> GetOfficesByUIC(string uic)
        {
            secdivDataContext db = new secdivDataContext();
            var offices = db.OFFICEs.Where(o => o.UIC.Equals(uic));
            if (offices.Count() > 0)
            {
                return offices.ToList();
            }
            return null;
        }

        //By the Office Name
        public List<OFFICE> GetOfficesByName(string name)
        {
            secdivDataContext db = new secdivDataContext();
            var offices = db.OFFICEs.Where(o => o.OfficeName.StartsWith(name));
            if (offices.Count() > 0)
            {
                return offices.ToList();
            }
            return null;
        }

        //By the Office Code
        public bool officeSearchResult { get; set; }
        public List<OFFICE> GetOfficesByOfficeCode(string code)
        {
            officeSearchResult = false;
            secdivDataContext db = new secdivDataContext();
            var offices = db.OFFICEs.Where(o => o.OFFICECODE.Equals(code));
            if (offices.Count() > 0)
            {
                officeSearchResult = true;
                return offices.ToList();
            }
            return null;
        }

        //By the Installation Code
        public List<OFFICE> GetOfficesByInstallationCode(string ic)
        {
            secdivDataContext db = new secdivDataContext();
            var offices = from o in db.OFFICEs
                          join u in db.Units
                          on o.UIC equals u.UIC
                          where u.INSTALLATIONCODE == ic
                          select o;
            if (offices.Count() > 0)
            {
                return offices.ToList();
            }
            return null;
        }

        //All of the Offices
        public List<OFFICE> GetAllOfOffices()
        {
            secdivDataContext db = new secdivDataContext();
            var offices = from o in db.OFFICEs
                          orderby o.OfficeName ascending
                          select o;
            if (offices.Count() > 0)
            {
                return offices.ToList();
            }
            return null;
        }
    }
}
