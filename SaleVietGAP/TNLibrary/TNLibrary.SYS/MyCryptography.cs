using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace TNLibrary.SYS
{
    public class MyCryptography
    {
        public static string HashPass(string nPass)
        {
            HashAlgorithm mHash = HashAlgorithm.Create("SHA1");

            byte[] pwordData = Encoding.Default.GetBytes(nPass);

            byte[] nHash = mHash.ComputeHash(pwordData);

            return Convert.ToBase64String(nHash);
        }

        public static Boolean VerifyHash(string NewPass, string OldPass)
        {
            string HashNewPass = HashPass(NewPass);
            return (OldPass == HashNewPass);
        }

    }
}
