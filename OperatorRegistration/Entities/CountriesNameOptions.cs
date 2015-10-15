using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OperatorRegistration.Entities
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// SCRIPTED DATE : 18 Jan 2012
    /// Entity - CountriesNameOptions
    /// </summary>
    public class CountriesNameOptions
    {
        public class Country
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }

        public List<Country> CountriesOptions { get; set; }
    }
}
