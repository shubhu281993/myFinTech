using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Security
{
    /// <summary>
	///     sealed to  prevent the inheritance of this class
	/// </summary>
	public sealed class Encryptor : IEncryptor
    {
        private static byte[] _key;
        private static byte[] _pwd;

        private Encryptor()
        {
            _key = new ASCIIEncoding().GetBytes("AAECAwQFBgcICQoLDA0ODw==");
            _pwd = new ASCIIEncoding().GetBytes("DELlSecIvPaSWord");
        }

        public static Encryptor Instance => NestedEncryptor._instance;

        /// <summary>
        ///     Encrypt a string value.  To decrypt this value, you must call the decrypt function.
        /// </summary>
        /// <returns>The encrypted value, represented as a hexadecimal string.</returns>
        public string Encrypt(string value)
        {
            var sbOut = new StringBuilder();

            try
            {
                var theStream = new MemoryStream();
                var rmCrypto = new RijndaelManaged();
                var cryptWrite = new CryptoStream(theStream, rmCrypto.CreateEncryptor(_key, _pwd), CryptoStreamMode.Write);
                cryptWrite.Write(Encoding.ASCII.GetBytes(value), 0, value.Length);
                cryptWrite.FlushFinalBlock();
                theStream.Seek(0, SeekOrigin.Begin);
                byte[] bytEncrypted = new byte[theStream.Length];
                theStream.Read(bytEncrypted, 0, (int)theStream.Length);
                foreach (var t in bytEncrypted)
                {
                    var strHexByte = t.ToString("X");
                    if (strHexByte.Length == 1)
                        sbOut.Append("0" + strHexByte);
                    else
                        sbOut.Append(strHexByte);
                }
                theStream.Close();
                cryptWrite.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Encrypt ERROR - {e.Message} - {e}");
            }
            return sbOut.ToString();
        }

        /// <summary>
        ///     Decrypt a string value.  This function can only be called on values encrypted using DDWEncrypt.encrypt.
        /// </summary>
        /// <returns>The decrypted value.Incase input value is plain text then plain text will be returned </returns>
        public string Decrypt(string value)
        {
            string strDecrypted;
            try
            {
                byte[] bytDecrypted = new byte[value.Length / 2];
                for (var x = 0; x < value.Length / 2; x++)
                    bytDecrypted[x] = byte.Parse(value.Substring(x * 2, 2), NumberStyles.AllowHexSpecifier);
                var theStream = new MemoryStream(bytDecrypted);
                var rmCrypto = new RijndaelManaged();
                theStream.Seek(0, SeekOrigin.Begin);
                var cryptRead = new CryptoStream(theStream, rmCrypto.CreateDecryptor(_key, _pwd), CryptoStreamMode.Read);
                TextReader sReader = new StreamReader(cryptRead);
                strDecrypted = sReader.ReadToEnd();
                theStream.Close();
                cryptRead.Close();
                sReader.Close();
            }
            catch (Exception)
            {
                strDecrypted = value;
                //Ignoring the error
                //Console.WriteLine($"Decrypt ERROR - {e}");
            }

            return strDecrypted;
        }

        private class NestedEncryptor
        {
            public static readonly Encryptor _instance = new Encryptor();

            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static NestedEncryptor()
            {
            }
        }
    }
}
