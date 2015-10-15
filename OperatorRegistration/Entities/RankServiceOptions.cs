using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OperatorRegistration.Entities
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// SCRIPTED DATE : 18 Jan 2012
    /// Entity - RankServiceOptions
    /// </summary>
    public class RankServiceOptions
    {
        public class RankService
        {
            public string Service { get; set; }
            public string Grade { get; set; }
            public string Rank { get; set; }
            public string RankGrade { get; set; }
            public string RankID { get; set; }
        }

        public List<RankService> RankOptions { get; set; }
    }
}
