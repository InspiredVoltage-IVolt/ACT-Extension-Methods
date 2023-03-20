using System.Text;

namespace ACT.Core.Extensions
{
    public static class Stream_Extensions
    {
        /// <summary>Converts a String to a MemoryStream.</summary>
        /// <param name="str">
        /// </param>
        /// <returns></returns>
        public static Stream ToStream(this string str) => new MemoryStream(Encoding.UTF8.GetBytes(str));

        /// <summary>Converts A Memory Stream To String</summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static string ConvertToString(this MemoryStream ms)
        {
            string str = "";
            using (StreamReader streamReader = new StreamReader(ms))
            {
                str = streamReader.ReadToEnd();
            }

            return str;
        }

        /// <summary>Converts A Normal Stream to a MemoryStream</summary>
        /// <param name="MainStream"></param>
        /// <returns></returns>
        public static MemoryStream ToMemoryStream(this Stream MainStream)
        {
            byte[] buffer = new byte[16384];
            MemoryStream memoryStream = new MemoryStream();
            int count;
            while ((count = MainStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                memoryStream.Write(buffer, 0, count);
            }

            return memoryStream;
        }


    }
}
