using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OperatorRegistration.Repositories;

namespace OperatorRegistration.Utilities
{
    public class GetUnit
    {
        /// <summary>
        /// Scripter : YONGTOK KIM
        /// Scripted Date : 3 Feb 2012
        /// Get the unit datas
        /// </summary>
        
        //By Installation Code
        public List<Unit> GetUnitsByInstallation(string ic)
        {

            secdivDataContext db = new secdivDataContext();
            var units = db.Units.Where(u => u.INSTALLATIONCODE.Equals(ic));
            if (units.Count() > 0)
            {
                return units.ToList();
            }
            return null;
        }

        //By Unit Name
        public List<Unit> GetUnitsByName(string name)
        {
            //string unitName = string.Format("%{0}%", name);
            secdivDataContext db = new secdivDataContext();
            //List<Unit> unit = db.GetUnitByUnitName(unitName);
            var units = db.Units.Where(u => u.UnitName.StartsWith(name));
            if (units.Count() > 0)
            {
                return units.ToList();
            }
            return null;
        }

        //By Unit Identification Code
        public List<Unit> GetUnitsByUIC(string uic)
        {
            secdivDataContext db = new secdivDataContext();
            var units = db.Units.Where(u => u.UIC.StartsWith(uic));
            if (units.Count() > 0)
            {
                return units.ToList();
            }
            return null;
        }

        //Search the Mached Unit By Unit Identification Code
        public bool ExistUnitMached(string uic)
        {
            bool isMached = false;
            secdivDataContext db = new secdivDataContext();
            var units = db.Units.Where(u => u.UIC.Equals(uic));
            if (units.Count() > 0)
                isMached = true;

            return isMached; 
        }

        //Get all of the units
        public List<Unit> GetUnitsAll()
        {
            secdivDataContext db = new secdivDataContext();
            var units = from u in db.Units
                        orderby u.UnitName ascending
                        select u;
            if (units.Count() > 0)
            {
                return units.ToList();
            }
            return null;
        }

        public Unit GetUnitByUIC(string uic)
        {
            secdivDataContext db = new secdivDataContext();
            var unit = db.Units.Where(u => u.UIC.Equals(uic));
            if (unit != null)
                return unit.First();
            return null;
        }
    }
}
