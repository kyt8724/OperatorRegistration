using System;

namespace OperatorRegistration.Utilities
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// Scripted Date : 17 Jan 2012  
    /// Validate KID
    /// </summary>
    public class ValidationOfKID
    {
        public bool ValidateKID(String number)
        {
            // check length
            if (number.Length != 13) return false;

            int sum = 0;

            // validate number or not
            int len = number.Length;
            for (int i = 0; i < len - 1; i++)
            {
                char c = number[i];
                if (!char.IsNumber(c)) return false;
                else
                {
                    // not calculate the last one
                    if (i < len) sum += int.Parse(c.ToString()) * ((i % 8) + 2);
                }
            }

            // Compare the sum and the last number of the KID
            if (((11 - (sum % 11)) % 10).ToString() == (number[number.Length - 1]).ToString())
                return true;
            return false;
        }
    }
}
