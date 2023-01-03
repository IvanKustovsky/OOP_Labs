using System.Security.Cryptography;
using System.Text;

namespace project.AccountFolder
{
    class Hashing
    {
        public static string GetHashPassword(string pass)
        {
            byte[] tmpSource = Encoding.ASCII.GetBytes(pass);
            byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            StringBuilder sOutput = new StringBuilder(tmpHash.Length);
            for (int i = 0; i < tmpHash.Length - 1; i++)
            {
                sOutput.Append(tmpHash[i].ToString("X2"));
            }
            return sOutput.ToString();
        }

    }
}
