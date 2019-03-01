using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace FitnessCenter_Project.Service
{
    public class DecryptService
    {
        private const string key = "468321dasggfsdf684321dsg486a4s135dfs8g41x84fg68zdb8gf84sd";
        //加密方法
        public string encrypt(string text)
        {
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(text);

            byte[] keyArray = null;

            string eKey = key;

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(eKey));

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;

            tdes.Mode = CipherMode.ECB;

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();

            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();

            string rtnStr = ByteArrayToString(resultArray);

            return rtnStr;
        }

        //解密方法
        public string decrypt(string text)
        {
            try
            {
                byte[] toEncryptArray = StringToByteArray(text);

                byte[] keyArray = null;

                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                string eKey = key;

                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(eKey));

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                tdes.Key = keyArray;

                tdes.Mode = CipherMode.ECB;

                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();

                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                tdes.Clear();

                string rtnStr = UTF8Encoding.UTF8.GetString(resultArray);

                return rtnStr;
            }
            catch (Exception)
            {
                return "";
            }
        }

        private static byte[] StringToByteArray(String hex)
        {
            if (hex.Substring(0, 2) == "0x")
            {
                hex = hex.Substring(2);
            }

            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }

        private static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);

            return hex.Replace("-", "");
        }

        public string sha256(string input)
        {
            System.Security.Cryptography.SHA256 sha = new System.Security.Cryptography.SHA256CryptoServiceProvider();
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = sha.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        protected string md5(string text)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = Encoding.UTF8.GetBytes(text);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes).Replace("-", "").ToLower();
        }
    }
}