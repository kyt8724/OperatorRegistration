using System.Text;
using System.Security.Cryptography;

namespace OperatorRegistration.Utilities
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// Scripted Date : 17 Jan 2012 
    /// Convert the string to the Hash
    /// </summary>
    public class DataHash
    {
        public static byte[] GetHashData(string src, string mode)
        {
            byte[] buffer = Encoding.Default.GetBytes(src);
            HashAlgorithm ha = HashAlgorithm.Create(mode);
            byte[] passwd = ha.ComputeHash(buffer);
            return passwd;
        }
    }
}
