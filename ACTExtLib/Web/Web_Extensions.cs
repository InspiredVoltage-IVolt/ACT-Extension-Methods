using System.Collections.Specialized;
using System.Net;

namespace ACT.Core.Extensions.Web
{
    public static class Web_Extensions
    {
        /// <summary>   A string extension method that returns get web request. </summary>
        /// <remarks>   Mark Alicz, 12/2021. </remarks>
        /// <param name="URL">              The URL to act on. </param>
        /// <param name="OptionalProxy">    (Optional) the optional proxy. </param>
        /// <returns>   The get web request. </returns>
        public static async Task<string> Return_GetWebRequest(this string URL, WebProxy? OptionalProxy = null)
        {
            if (OptionalProxy != null)
            {
                HttpClient.DefaultProxy = OptionalProxy;
            }

            HttpClient httpClient = new HttpClient();
            var _Results = await httpClient.GetStringAsync(URL);

            return _Results.ToString(true);
        }


        public static string CombineQuerystringToString(this NameValueCollection QueryString)
        {
            string x = "";
            foreach (object key in QueryString.Keys)
            {
                x = x + key + "=" + QueryString[key.ToString()] + "&";
            }

            return x.TrimEnd("&");
        }

    }
}
