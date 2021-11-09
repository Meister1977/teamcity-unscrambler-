using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Teamcity_Unscrambler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(Decrypt(args[0]));
        }

        public static string Decrypt(string encodedText)
        {
            sbyte[] keySignedByteArray =
                { 61, 22, 11, 57, 110, 89, -20, -1, 0, 99, 111, -120, 55, 4, -9, 10, 11, 45, 71, -89, 21, -99, 54, 51 };

            var keyByteArray = keySignedByteArray.Select(x => (byte)(x & 0xff)).ToArray();

            var desCryptoProvider = new TripleDESCryptoServiceProvider
            {
                Key = keyByteArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.None
            };

            var byteBuff = Convert.FromHexString(encodedText[3..]);

            return Encoding.UTF8.GetString(desCryptoProvider.CreateDecryptor()
                .TransformFinalBlock(byteBuff, 0, byteBuff.Length));
        }
    }
}