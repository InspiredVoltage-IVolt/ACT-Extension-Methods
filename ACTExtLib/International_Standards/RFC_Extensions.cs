using ACT.Core.Constants;
using System.Text;

namespace ACT.Core.Extensions
{
    public static class RFC_Extensions
    {
        /// <summary>   Escapes a string according to the URI data string rules given in RFC 3986. </summary>
        /// <remarks>
        /// The <see cref="M:System.Uri.EscapeDataString(System.String)" /> method is <i>supposed</i> to take on RFC 3986 behavior
        /// if certain elements are present in a .config file.  Even if this actually worked (which in my
        /// experiments it <i>doesn't</i>), we can't rely on every host actually having this
        /// configuration element present.
        /// </remarks>
        /// <param name="value">    The value to escape. </param>
        /// <returns>   The escaped value. </returns>
        internal static string EscapeUriDataStringRfc3986(this string value)
        {
            StringBuilder stringBuilder = new StringBuilder(Uri.EscapeDataString(value));
            for (int index = 0; index < International_Standards.UriRfc3986CharsToEscape.Length; ++index)
            {
                stringBuilder.Replace(International_Standards.UriRfc3986CharsToEscape[index], Uri.HexEscape(International_Standards.UriRfc3986CharsToEscape[index][0]));
            }

            return stringBuilder.ToString();
        }
    }
}
