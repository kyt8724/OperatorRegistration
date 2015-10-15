
namespace OperatorRegistration.Utilities
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// Scripted Date : 17 Jan 2010
    /// Return the Full Name
    /// </summary>
    public class MakeFullName
    {
        public string MakingFullName(string lName, string fName, string mName)
        {
            string name = string.Format("{0}, {1} {2}", lName, fName, mName);

            if (!string.IsNullOrEmpty(name))
                return name;
            return null;
        }

        public string MakingFullName(string lName, string fName)
        {
            string name = string.Format("{0}, {1}", lName, fName);

            if (!string.IsNullOrEmpty(name))
                return name;
            return null;
        }
    }
}
