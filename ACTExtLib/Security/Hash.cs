using System.Text;
using System.Security.Cryptography;

namespace ACT.Core.Extensions.Security
{


    public static class Hashing
    {
        /// <summary>
        /// Not All hash algorithms supported yet.  
        /// Supported Ones are MD5, SHA1, SHA256, SHA384, SHA512
        /// </summary>
        public enum eHashType
        {
            HMAC, HMACMD5, HMACSHA1, HMACSHA256, HMACSHA384, HMACSHA512,
            MACTripleDES,
            MD5, RIPEMD160, SHA1, SHA256, SHA384, SHA512
        }

        private static byte[] GetHash(string input, eHashType hash)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);

            switch (hash)
            {
                case eHashType.HMAC:
                    throw new Exception("Not Implemented Yet");

                case eHashType.HMACMD5:
                    throw new Exception("Not Implemented Yet");

                case eHashType.HMACSHA1:
                    throw new Exception("Not Implemented Yet");

                case eHashType.HMACSHA256:
                    throw new Exception("Not Implemented Yet");

                case eHashType.HMACSHA384:
                    throw new Exception("Not Implemented Yet");

                case eHashType.HMACSHA512:
                    throw new Exception("Not Implemented Yet");

                case eHashType.MACTripleDES:
                    throw new Exception("Not Implemented Yet");

                case eHashType.MD5:
                    return MD5.Create().ComputeHash(inputBytes);

                case eHashType.RIPEMD160:
                    throw new Exception("Not Implemented Yet");

                case eHashType.SHA1:
                    return SHA1.Create().ComputeHash(inputBytes);

                case eHashType.SHA256:
                    return SHA256.HashData(inputBytes);

                case eHashType.SHA384:
                    return SHA384.Create().ComputeHash(inputBytes);

                case eHashType.SHA512:
                    return SHA512.Create().ComputeHash(inputBytes);

                default:
                    return inputBytes;
            }
        }

        /// <summary>
        /// Computes the hash of the string using a specified hash algorithm
        /// </summary>
        /// <param name="input">The string to hash</param>
        /// <param name="hashType">The hash algorithm to use</param>
        /// <returns>The resulting hash or an empty string on error</returns>
        public static string ComputeHash(this string input, eHashType hashType)
        {            
            try
            {
                byte[] hash = GetHash(input, hashType);
                StringBuilder ret = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                    ret.Append(hash[i].ToString("x2"));

                return ret.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
