using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using OperatorRegistration.Entities;

namespace OperatorRegistration.Utilities
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// SCRIPTED DATE : 18 Jan 2012
    /// Get datas from xml file
    /// </summary>
    public class DeserializeXml
    {
        private string root = Application.StartupPath;
        private XmlSerializer serializer;
        private FileStream fs;

        public PersonalInfoOptions DeserialzeObject()
        {
            string filename = Path.Combine(root, @"XMLData\\PersonalInfoOptions.xml");
            serializer = new XmlSerializer(typeof(PersonalInfoOptions));

            fs = new FileStream(filename, FileMode.Open);

            PersonalInfoOptions PIOptions = (PersonalInfoOptions)serializer.Deserialize(fs);
            fs.Close();

            return PIOptions;
        }

        public InstallationNameOptions DeserializeInstallation()
        {
            string filename = Path.Combine(root, @"XMLData\\Installation.xml");
            serializer = new XmlSerializer(typeof(InstallationNameOptions));

            fs = new FileStream(filename, FileMode.Open);

            InstallationNameOptions INOptions = (InstallationNameOptions)serializer.Deserialize(fs);
            fs.Close();

            return INOptions;
        }

        public CountriesNameOptions DeserializeCountry()
        {
            string filename = Path.Combine(root, @"XMLData\\Countries.xml");
            serializer = new XmlSerializer(typeof(CountriesNameOptions));

            fs = new FileStream(filename, FileMode.Open);

            CountriesNameOptions countries = (CountriesNameOptions)serializer.Deserialize(fs);
            fs.Close();

            return countries;
        }

        public RankServiceOptions DeserializeRank()
        {
            string filename = Path.Combine(root, @"XMLData\\RankService.xml");
            serializer = new XmlSerializer(typeof(RankServiceOptions));

            fs = new FileStream(filename, FileMode.Open);

            RankServiceOptions RSOptions = (RankServiceOptions)serializer.Deserialize(fs);
            fs.Close();

            return RSOptions;
        }

        public OtherOptions DeserializeOtherOptions()
        {
            string filename = Path.Combine(root, @"XMLData\\OtherOption.xml");
            serializer = new XmlSerializer(typeof(OtherOptions));

            fs = new FileStream(filename, FileMode.Open);

            OtherOptions otherOptions = (OtherOptions)serializer.Deserialize(fs);
            fs.Close();

            return otherOptions;
        }

        public UserTypes DeserializeUserTypeOptions()
        {
            string filename = Path.Combine(root, @"XMLData\\UserType.xml");
            serializer = new XmlSerializer(typeof(UserTypes));

            fs = new FileStream(filename, FileMode.Open);

            UserTypes userTypeOptions = (UserTypes)serializer.Deserialize(fs);
            fs.Close();

            return userTypeOptions;
        }

        public PersonAndDbidsOperators DeserializePersons()
        {
            string filename = Path.Combine(root, @"Data\PersonAndDbidsOperators.xml");
            if (File.Exists(filename))
            {
                serializer = new XmlSerializer(typeof(PersonAndDbidsOperators));

                fs = new FileStream(filename, FileMode.Open);

                PersonAndDbidsOperators persons = (PersonAndDbidsOperators)serializer.Deserialize(fs);
                fs.Close();

                return persons;
            }
            return null;
        }
    }
}
