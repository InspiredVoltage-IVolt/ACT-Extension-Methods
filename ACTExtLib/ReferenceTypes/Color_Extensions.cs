using System.Drawing;
using System.Text.RegularExpressions;

namespace ACT.Core.Extensions
{
    /// <summary>   Holds Extension Methods For System.Drawing.Color objects. </summary>
    /// <remarks>   Mark Alicz, 11/22/2016. </remarks>
    public static class SystemDrawingColorExtensions
    {
        /// <summary>
        /// A System.Drawing.Color extension method that converts a c to a hexadecimal string.
        /// </summary>
        /// <remarks>   Mark Alicz, 11/22/2016. </remarks>
        /// <param name="c">    The Color to process. </param>
        /// <returns>Hex Value Of the Color.</returns>
        public static string ToHexString(this Color c)
        {
            byte num = c.R;
            string str1 = num.ToString("X2");
            num = c.G;
            string str2 = num.ToString("X2");
            num = c.B;
            string str3 = num.ToString("X2");
            return str1 + str2 + str3;
        }

        public static bool CheckValidFormatHtmlColor(this string inputColor) => Regex.Match(inputColor, ACT.Core.Constants.RegularExpressions.ColorHex).Success;

        /// <summary>
        /// Get Color from a Hex String
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Color? GetImageFromHexString(this string x)
        {
            if (!x.CheckValidFormatHtmlColor())
            {
                if (!x.StartsWith("#"))
                {
                    x = "#" + x;
                }

                if (!x.CheckValidFormatHtmlColor())
                {
                    return new Color?();
                }
            }
            return new Color?(ColorTranslator.FromHtml(x));
        }
    }
}
