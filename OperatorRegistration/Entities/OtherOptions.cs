using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OperatorRegistration.Entities
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// SCRIPTED DATE : 18 Jan 2012
    /// Entity - OtherOptions
    /// </summary>
    public class OtherOptions
    {
        public class OperatorOption
        {
            public string Value { get; set; }
            public string Operator { get; set; }
        }

        public class CertificateStatusOption
        {
            public string Value { get; set; }
            public string CertificateStatus { get; set; }
        }

        public class PersonStatusOption
        {
            public string Value { get; set; }
            public string PersonStatus { get; set; }
        }

        public class TrainingStatusOption
        {
            public string Value { get; set; }
            public string TrainingStatus { get; set; }
        }

        public class ClassTypeOption
        {
            public string Value { get; set; }
            public string ClassType { get; set; }
        }

        public class TestResultOption
        {
            public string Value { get; set; }
            public string TestResult { get; set; }
        }

        public class EmailServerOption
        {
            public string Value { get; set; }
            public string EmailServer { get; set; }
        }

        public List<OperatorOption> OperatorOptions { get; set; }
        public List<CertificateStatusOption> CertificateStatusOptions { get; set; }
        public List<PersonStatusOption> PersonStatusOptions { get; set; }
        public List<TrainingStatusOption> TrainingStatusOptions { get; set; }
        public List<ClassTypeOption> ClassTypeOptions { get; set; }
        public List<TestResultOption> TestResultOptions { get; set; }
        public List<EmailServerOption> EmailServerOptions { get; set; }
    }
}
