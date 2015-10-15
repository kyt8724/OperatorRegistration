using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OperatorRegistration.Entities
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// SCRIPTED DATE : 18 Jan 2012
    /// Entity - PersonalInfoOptions
    /// </summary>
    public class PersonalInfoOptions
    {
        public class GenderOption
        {
            public string Value { get; set; }
            public string Gender { get; set; }
        }

        public class IDTypeOption
        {
            public string Value { get; set; }
            public string IDType { get; set; }
        }

        public class HairColorOption
        {
            public string Value { get; set; }
            public string HairColor { get; set; }
        }

        public class EyeColorOption
        {
            public string Value { get; set; }
            public string EyeColor { get; set; }
        }

        public class MobilePhoneCode
        {
            public string Value { get; set; }
            public string MobilePhone { get; set; }
        }

        public class PhoneAreaCode
        {
            public string Value { get; set; }
            public string PhoneArea { get; set; }
        }

        public class ServiceOption
        {
            public string Value { get; set; }
            public string Service { get; set; }
        }

        public List<GenderOption> GenderOptions { get; set; }
        public List<IDTypeOption> IDTypeOptions { get; set; }
        public List<HairColorOption> HairColorOptions { get; set; }
        public List<EyeColorOption> EyeColorOptions { get; set; }
        public List<MobilePhoneCode> MobilePhoneCodes { get; set; }
        public List<PhoneAreaCode> PhoneAreaCodes { get; set; }
        public List<ServiceOption> ServiceOptions { get; set; }
    }
}
