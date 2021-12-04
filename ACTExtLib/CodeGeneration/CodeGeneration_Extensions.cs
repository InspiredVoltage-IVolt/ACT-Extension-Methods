using System.Text.RegularExpressions;


namespace ACT.Core.Extensions.CodeGeneration
{
    public static class CodeGeneration_Extensions
    {
        /// <summary>   A string extension method that converts an x to a C# friendly name. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">    The x to act on. </param>
        /// <returns>   x as a string. </returns>
        public static string ToCSharpFriendlyName(this string x)
        {
            if (!Regex.Match(x, "^[A-Za-z]").Success)
            {
                x = "_" + x;
            }

            x = x.Replace("$", "dollar");
            x = x.Replace("-", "dash");
            x = x.Replace(" ", "_");
            x = x.Replace("!", "exclamation");
            x = x.Replace("@", "at");
            x = x.Replace("#", "pound");
            x = x.Replace("%", "percent");
            x = x.Replace("^", "carrot");
            x = x.Replace("&", "and");
            x = x.Replace("*", "times");
            x = x.Replace("(", "leftparent");
            x = x.Replace(")", "rightparent");
            x = x.Replace("?", "questionmark");
            x = x.Replace("/", "slash");
            x = x.Replace("\\", "backslash");
            foreach (string reservedKeyword in Constants.CSharp.__ReservedKeywords)
            {
                if (reservedKeyword == x)
                {
                    x += "_R";
                }
            }
            return x;
        }
    }
}
