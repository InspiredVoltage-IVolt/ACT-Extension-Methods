using System.Text;

namespace ACT.Core.Extensions
{
    public static class Stream_Extensions
    {
        /// <summary>Converts a String to a MemoryStream.</summary>
        /// <param name="str">
        /// </param>
        /// <returns></returns>
        public static Stream ToStream(this string str) => new MemoryStream(Encoding.ASCII.GetBytes(str));
    }
}
