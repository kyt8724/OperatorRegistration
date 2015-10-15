using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OperatorRegistration.Entities;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;

namespace OperatorRegistration.Utilities
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// SCRIPTED DATE : 18 Jan 2012
    /// Save datas to xml file
    /// </summary>
    class SerializeXml
    {
        private string root = Application.StartupPath;
        private XmlSerializer serializer;
        private FileStream fs;

        public void WriteXml(PersonAndDbidsOperators operators)
        {
            string filename = Path.Combine(root, @"Data\PersonAndDbidsOperators.xml");
            serializer = new XmlSerializer(typeof(PersonAndDbidsOperators));

            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            fs = new FileStream(filename, FileMode.Create);
            serializer.Serialize(fs, operators);
            fs.Close();
        }
    }
}
