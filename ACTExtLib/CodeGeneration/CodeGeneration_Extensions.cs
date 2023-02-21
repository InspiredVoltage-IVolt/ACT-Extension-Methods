using System.Text.RegularExpressions;


namespace ACT.Core.Extensions.CodeGeneration
{
    public static class CodeGeneration_Extensions
    {
        /// <summary>   The reserved keywords. </summary>
        public static string[] __ReservedKeywords = new string[] { "abstract", "as", "base", "bool", "break", "by", "case", "catch", "char", "checked", "class", "con", "continue", "decimal", "default", "delegate", "do", "doub", "else", "enum", "event", "explicit", "extern", "fal", "finally", "fixed", "float", "for", "foreach", "go", "if", "implicit", "in", "int", "interface", "intern", "is", "lock", "long", "namespace", "new", "nu", "object", "operator", "out", "override", "params", "priva", "protected", "public", "readonly", "ref", "return", "sby", "sealed", "short", "sizeof", "stackalloc", "static", "stri", "struct", "switch", "this", "throw", "true", "t", "typeof", "uint", "ulong", "unchecked", "unsafe", "usho", "using", "virtual", "void", "volatile", "whi", "System", "ACT", "Microsoft", "Core" };

        /// <summary> The reserved keywords. </summary>
        private static readonly List<string> _reservedKeywords = new List<string>();

        #region Documentation
        /// Property:   ReservedKeywords
        /// Summary:    Gets the reserved keywords.
        /// Returns:    The reserved keywords.
        #endregion
        public static List<string> ReservedKeywords
        {
            get
            {
                if (_reservedKeywords.Count == 0) { _reservedKeywords.AddRange(__ReservedKeywords); }
                return _reservedKeywords;
            }
        }

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

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   To MSSQL Friendly Name. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="x">    String To Validate. </param>
        ///
        /// <returns>   Validated String. </returns>
        ///-------------------------------------------------------------------------------------------------
        public static string ToMSSQLFriendlyName(this string x)
        {
            string[] _AllowedCharacters = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,_,@,#,$".SplitString(",", StringSplitOptions.RemoveEmptyEntries);

            string _replacement = "";
            foreach (char c in x.ToArray())
            {
                if (_AllowedCharacters.Contains(c.ToString()) == true) { _replacement = _replacement + c; }
                else { _replacement = _replacement + "_"; }
            }
            return _replacement;
        }
    }
}
