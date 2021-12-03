using System.Text;

namespace ACT.Core.Extensions
{
    public static class Char_Extensions
    {
        /// <summary>Get Bytes from the Char[]</summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static byte[] GetBytes(this char[] x) => Encoding.Unicode.GetBytes(x);
    }
}
