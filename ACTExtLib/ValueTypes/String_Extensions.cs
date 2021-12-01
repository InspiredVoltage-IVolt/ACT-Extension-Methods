using Newtonsoft.Json;
using System.Data;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.XPath;

namespace ACT.Core.Extensions
{
    public static class String_Extensions
    {


        /// <summary>
        /// Searches the string X for any instance of the List of Strings
        /// </summary>
        /// <param name="x"></param>
        /// <param name="SearchStrings"></param>
        /// <param name="IgnoreCase"></param>
        /// <returns></returns>
        public static bool Contains(this string x, List<string> SearchStrings, bool IgnoreCase)
        {
            string str1 = x;
            if (IgnoreCase)
            {
                str1 = str1.ToLower();
            }

            foreach (string searchString in SearchStrings)
            {
                string str2 = searchString;
                if (IgnoreCase)
                {
                    str2 = searchString.ToLower();
                }

                if (str1.Contains(str2))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Searches the string X for any instance of the List of Strings
        /// </summary>
        /// <param name="x"></param>
        /// <param name="SearchString"></param>
        /// <param name="IgnoreCase"></param>
        /// <returns></returns>
        public static bool Contains(this string x, string SearchString, bool IgnoreCase = true)
        {
            string str1 = x;
            if (IgnoreCase)
            {
                str1 = str1.ToLower();
            }

            string str2 = SearchString;
            if (IgnoreCase)
            {
                str2 = SearchString.ToLower();
            }

            return str1.Contains(str2);
        }

        /// <summary>
        /// Searches the string X for any instance of the List of Strings
        /// </summary>
        /// <param name="x"></param>
        /// <param name="SearchString"></param>
        /// <param name="IgnoreCase"></param>
        /// <returns></returns>
        public static bool Contains(this List<string> x, string SearchString, bool IgnoreCase = true)
        {
            foreach (string str1 in x)
            {
                string str2 = str1;
                if (IgnoreCase)
                {
                    str2 = str1.ToLower();
                }

                string str3 = SearchString;
                if (IgnoreCase)
                {
                    str3 = SearchString.ToLower();
                }

                if (str2.Contains(str3))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// RPL - Replace Shorthand
        /// </summary>
        /// <param name="x"></param>
        /// <param name="Find"></param>
        /// <param name="Replace"></param>
        /// <returns></returns>
        public static string RPL(this string x, string Find, string Replace) => x.Replace(Find, Replace);

        /// <summary>
        /// Escapes a FileName to Windows Specifications
        /// </summary>
        /// <param name="x"></param>
        /// <param name="R"></param>
        /// <returns></returns>
        public static string EscapeFileName(this string x, string R = "_")
        {
            x = x.Trim("_");
            x = x.Trim(".");
            x = x.Trim("..");
            return x.RPL("~", R).RPL("#", R).RPL("%", R).RPL("&", R).RPL("*", R).RPL("{", R).RPL("}", R).RPL("\\", R).RPL(":", R).RPL("<", R).RPL(">", R).RPL("?", R).RPL("/", R).RPL("+", R).RPL("|", R).RPL("\"", R);
        }

        /// <summary>   To String Overload Convert Null To Empty String. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">                        string To Convert. </param>
        /// <param name="ConvertNullToEmptyString"> True/False Convert Null To Empty String. </param>
        /// <returns>   A string that represents this object. </returns>
        public static string ToString(this string x, bool ConvertNullToEmptyString) => ConvertNullToEmptyString && x == null ? "" : x;

        /// <summary>   Converts The String To UTF 8 Format. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">    String To Convert. </param>
        /// <returns>   UTF 8 String. </returns>
        public static string ToUTF8(this string x) => System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(x));

        /// <summary>   URL Encode The String. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">                String To URL Encode. </param>
        /// <param name="UseDataMethod">    (Optional) Use Data Method (EscapeDataString) VS
        /// (EscapeUriString) </param>
        /// <returns>   URL Encoded String. </returns>
        public static string URLEncode(this string x, bool UseDataMethod = true) => UseDataMethod ? Uri.EscapeDataString(x) : Uri.EscapeUriString(x);

        /// <summary>Decodes a URL Encoded String</summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static string URLDecode(this string x) => HttpUtility.UrlDecode(x);

        /// <summary>   A string extension method that ensures that starts with. </summary>
        /// <remarks>   Mark Alicz, 9/8/2019. </remarks>
        /// <param name="x">    string To Convert. </param>
        /// <param name="Start">  The end. </param>
        /// <returns>   A string. </returns>
        public static string EnsureStartsWith(this string x, string Start) => !x.StartsWith(Start) ? x + Start : x;

        /// <summary>   A string extension method that ensures that ends with. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">    string To Convert. </param>
        /// <param name="End">  The end. </param>
        /// <returns>   A string. </returns>
        public static string EnsureEndsWith(this string x, string End) => !x.EndsWith(End) ? x + End : x;

        /// <summary>
        /// Checks the string to make sure the string ends with one of the specified options
        /// </summary>
        /// <param name="x"></param>
        /// <param name="Options"></param>
        /// <returns></returns>
        public static bool EndsWith(this string x, List<string> Options, bool CaseSensitive = false)
        {
            if (!CaseSensitive)
            {
                x = x.ToLower();
            }

            foreach (string option in Options)
            {
                if (!CaseSensitive)
                {
                    if (x.EndsWith(option.ToLower()))
                    {
                        return true;
                    }
                }
                else if (x.EndsWith(option))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>   Tests The Value Returns True/False Depending on Match. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">        . </param>
        /// <param name="Default">  (Optional) </param>
        /// <returns>   The given data converted to a bool. </returns>
        /// 
        ///             
        ///              ### <param name="CaseSensitive">    . </param>
        public static bool ToBool(this string x, bool Default = false)
        {
            if (x == null)
            {
                return Default;
            }

            x = x.Trim();
            try
            {
                return x.ToLower() == "yes" || x.ToLower() == "true" || x.ToLower() == "1";
            }
            catch
            {
                return Default;
            }
        }

        /// <summary>   Tests The Value Returns True/False Depending on Match. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">                . </param>
        /// <param name="TestCaseYes">      . </param>
        /// <param name="CaseSensitive">    (Optional) </param>
        /// <returns>   true if the test passes, false if the test fails. </returns>
        public static bool TestToBool(this string x, string TestCaseYes, bool CaseSensitive = false) => CaseSensitive ? x.ToString() == TestCaseYes : x.ToString().ToLower() == TestCaseYes.ToLower();

        /// <summary>   Converts A String To A DateTime.  If Error Returns Null. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">    . </param>
        /// <returns>   DateTime or Null. </returns>
        public static DateTime? TryToDateTime(this string x)
        {
            try
            {
                return new DateTime?(Convert.ToDateTime(x));
            }
            catch
            {
                return new DateTime?();
            }
        }

        /// <summary>
        /// Normalize A String Value For SQL Server.  Used Specifically For Non Parameterized SQL
        /// Queries.  i.e. Values of Insert Statements Also Attempts To Remove Any SQL Injections
        /// Attempts.
        /// </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">    String To Normalize. </param>
        /// <returns>   Normalized String. </returns>
        public static string NormalizeForSQLServer(this string x) => x.Replace("'", "''").Replace("' OR", "").Replace("' AND", "");

        /// <summary>   Converts A String To A Int.  If Error Returns Null. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">    . </param>
        /// <returns>   Int or Null. </returns>
        public static int? TryToInt(this string x)
        {
            try
            {
                return new int?(Convert.ToInt32(x));
            }
            catch
            {
                return new int?();
            }
        }

        /// <summary>   Converts a string to a Guid. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">    . </param>
        /// <returns>   Guid or Null. </returns>
        public static Guid? TryToGuid(this string x)
        {
            try
            {
                return new Guid?(new Guid(x));
            }
            catch
            {
                return new Guid?();
            }
        }

        /// <summary>   Converts A String To A Decimal.  If Error Returns Null. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">    . </param>
        /// <returns>   Decimal or Null. </returns>
        public static Decimal? TryToDecimal(this string x)
        {
            try
            {
                return new Decimal?(Convert.ToDecimal(x));
            }
            catch
            {
                return new Decimal?();
            }
        }

        /// <summary>   Converts A String To A Decimal.  If Error Returns Null. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">    . </param>
        /// <returns>   Decimal or Null. </returns>
        public static double? TryToDouble(this string x)
        {
            try
            {
                return new double?(Convert.ToDouble(x));
            }
            catch
            {
                return new double?();
            }
        }

        /// <summary>   A string extension method that format to money. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">    string To Convert. </param>
        /// <returns>   The formatted to money. </returns>
        public static string Format_To_Money(this string x)
        {
            try
            {
                string str = string.Format("{0:C}", Convert.ToDouble(x));
                return str.Substring(0, str.IndexOf("."));
            }
            catch
            {
                return "$0";
            }
        }

        /// <summary>   A byte[] extension method that gets a string. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="buffer">   The buffer to act on. </param>
        /// <returns>   The string. </returns>
        public static string GetString(this byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0)
            {
                return "";
            }

            System.Text.Encoding encoding = System.Text.Encoding.Default;
            if (buffer[0] == 239 && buffer[1] == 187 && buffer[2] == 191)
            {
                encoding = System.Text.Encoding.UTF8;
            }
            else if (buffer[0] == 254 && buffer[1] == byte.MaxValue)
            {
                encoding = System.Text.Encoding.Unicode;
            }
            else if (buffer[0] == 254 && buffer[1] == byte.MaxValue)
            {
                encoding = System.Text.Encoding.BigEndianUnicode;
            }
            else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 254 && buffer[3] == byte.MaxValue)
            {
                encoding = System.Text.Encoding.UTF32;
            }
            else if (buffer[0] == 43 && buffer[1] == 47 && buffer[2] == 118)
            {
                encoding = System.Text.Encoding.UTF7;
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(buffer, 0, buffer.Length);
                memoryStream.Seek(0L, SeekOrigin.Begin);
                using (StreamReader streamReader = new StreamReader(memoryStream, encoding))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }


        /// <summary>
        /// A string extension method that creates nice spaced text from single text capitals.
        /// </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="val">  The val to act on. </param>
        /// <returns>   The new nice spaced text from single text capitals. </returns>
        public static string CreateNiceSpacedTextFromSingleTextCapitals(this string val)
        {
            val = string.Concat(val.Select<char, string>(x => !char.IsUpper(x) ? x.ToString() : " " + x.ToString())).TrimStart(' ');
            return val;
        }

        /// <summary>   A string extension method that string between. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">        string To Convert. </param>
        /// <param name="Start">    The start. </param>
        /// <param name="End">      The end. </param>
        /// <returns>   A string. </returns>
        public static string StringBetween(this string x, string Start, string End)
        {
            int startIndex = x.IndexOf(Start) + Start.Length;
            if (startIndex == -1)
            {
                return "";
            }

            int num = x.IndexOf(End, startIndex);
            return num == -1 ? "" : x.Substring(startIndex, num - startIndex);
        }

        /// <summary>Converts a byte array to a string</summary>
        /// <param name="bytes">the byte array</param>
        /// <returns>The string</returns>
        public static byte[] FromBase64String(this string base64String) => Convert.FromBase64String(base64String);

        /// <summary>
        /// Replace Extension (Child) Accepts Comma Seperated Strings as Parameters for the Lazy People.
        /// </summary>
        /// <remarks>   Mark Alicz, 11/24/2016. </remarks>
        /// <param name="str">                          The str to act on. </param>
        /// <param name="replaceCommaSeperated">        The replace comma seperated. </param>
        /// <param name="replacewithCommaSeperated">    The replacewith comma seperated. </param>
        /// <param name="comparison">                   The comparison method. </param>
        /// <returns>   A string. </returns>
        public static string Replace(
          this string str,
          string replaceCommaSeperated,
          string replacewithCommaSeperated,
          StringComparison comparison)
        {
            return str.Replace(replaceCommaSeperated.SplitString(",", StringSplitOptions.RemoveEmptyEntries), replacewithCommaSeperated.SplitString(",", StringSplitOptions.None), comparison);
        }

        /// <summary>   A string extension method that replaces strings. </summary>
        /// <remarks>   Mark Alicz, 11/24/2016. </remarks>
        /// <param name="str">          The str to act on. </param>
        /// <param name="replace">      The Old Value strings string[]. </param>
        /// <param name="replacewith">  The New Value strings string[]. </param>
        /// <param name="comparison">   The comparison method. </param>
        /// <returns>   A string. </returns>
        public static string Replace(
          this string str,
          string[] replace,
          string[] replacewith,
          StringComparison comparison)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string str1 = str;
            for (int index1 = 0; index1 < replace.Length; ++index1)
            {
                string str2 = replace[index1];
                string str3 = replacewith[index1];
                int startIndex1 = 0;
                int startIndex2;
                for (int index2 = str1.IndexOf(str2, comparison); index2 != -1; index2 = str1.IndexOf(str2, startIndex2, comparison))
                {
                    stringBuilder.Append(str1.Substring(startIndex1, index2 - startIndex1));
                    stringBuilder.Append(str3);
                    startIndex2 = index2 + str2.Length;
                    startIndex1 = startIndex2;
                }
                stringBuilder.Append(str1.Substring(startIndex1));
                str1 = stringBuilder.ToString();
                stringBuilder.Clear();
            }
            return str1;
        }

        /// <summary>   A string extension method that removes the script tags described by x. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">    string To Convert. </param>
        /// <returns>   A string. </returns>
        public static string RemoveScriptTags(this string x) => x.Replace("<script>", "").Replace("</script>", "").Replace("</Script>", "").Replace("</SCRIPT>", "").Replace("</SCript>", "").Replace("<Script>", "").Replace("<SCRIPT>", "").Replace("<SCript>", "");

        /// <summary>   A string extension method that converts an x to a JSON. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <exception cref="T:System.Exception">    Thrown when an exception error condition occurs. </exception>
        /// <param name="x">    string To Convert. </param>
        /// <returns>   x as a dynamic. </returns>
        public static object ToJSON(this string x)
        {
            try
            {
                return JsonConvert.DeserializeObject<object>(x);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading JSON", ex);
            }
        }

        public static T ToType<T>(this string x, string defaultValue)
        {
            if (typeof(T) == typeof(uint) || typeof(T) == typeof(uint))
            {
                return (T)Convert.ChangeType(x.ToInt(ToInt(defaultValue, 0)), typeof(T));
            }

            if (typeof(T) == typeof(long) || typeof(T) == typeof(ulong))
            {
                return (T)Convert.ChangeType(x.ToLong(ToLong(defaultValue, 0L)), typeof(T));
            }

            if (typeof(T) == typeof(bool))
            {
                return (T)Convert.ChangeType(x.ToBool(ToBool(defaultValue)), typeof(T));
            }

            if (typeof(T) == typeof(DateTime))
            {
                return (T)Convert.ChangeType(x.ToDateTime(defaultValue.ToDateTime(DateTime.MinValue)), typeof(T));
            }

            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(x, typeof(string));
            }

            if (typeof(T) == typeof(Decimal))
            {
                return (T)Convert.ChangeType(x.ToDecimal(ToDecimal(defaultValue, 0M)), typeof(Decimal));
            }

            return typeof(T) == typeof(Decimal) ? (T)Convert.ChangeType(x.ToFloat(defaultValue.ToFloat()), typeof(float)) : default(T);
        }

        /// <summary>   A string extension method that converts an x to a base 64. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">    string To Convert. </param>
        /// <returns>   x as a string. </returns>
        public static string ToBase64(this string x) => Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(x));

        /// <summary>
        /// A string extension method that initializes this object from the given from base 64.
        /// </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">    string To Convert. </param>
        /// <returns>   A string. </returns>
        public static string FromBase64(this string x) => System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(x));


        /// <summary>   A string extension method that converts an x to an invariant string. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">    string To Convert. </param>
        /// <returns>   x as a string. </returns>
        public static string ToInvariantString(this string x) => x.ToString(CultureInfo.InvariantCulture);

        /// <summary>   A string extension method that converts an x to an int fast. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">    string To Convert. </param>
        /// <returns>   x as an int. </returns>
        public static int ToIntFast(this string x)
        {
            int num = 0;
            for (int index = 0; index < x.Length; ++index)
            {
                num = num * 10 + (x[index] - 48);
            }

            return num;
        }

        /// <summary>   A string extension method that converts this object to an int. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">    string To Convert. </param>
        /// <returns>   The given data converted to an int. </returns>
        public static int ToInt(this string x) => Convert.ToInt32(x);

        /// <summary>   A string extension method that converts this object to a long. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">    string To Convert. </param>
        /// <returns>   The given data converted to a long. </returns>
        public static long ToLong(this string x) => Convert.ToInt64(x);

        /// <summary>   A string extension method that converts this object to an int. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">            string To Convert. </param>
        /// <param name="ErrorValue">   The error value Date/Time. </param>
        /// <returns>   The given data converted to an int. </returns>
        public static int ToInt(this string x, int ErrorValue)
        {
            try
            {
                return Convert.ToInt32(x);
            }
            catch
            {
                return ErrorValue;
            }
        }

        /// <summary>   A string extension method that converts this object to a long. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">            string To Convert. </param>
        /// <param name="ErrorValue">   The error value Date/Time. </param>
        /// <returns>   The given data converted to a long. </returns>
        public static long ToLong(this string x, long ErrorValue)
        {
            try
            {
                return Convert.ToInt64(x);
            }
            catch
            {
                return ErrorValue;
            }
        }

        /// <summary>   A string extension method that converts this object to a decimal. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">            string To Convert. </param>
        /// <param name="ErrorValue">   The error value Date/Time. </param>
        /// <returns>   The given data converted to a decimal. </returns>
        public static Decimal ToDecimal(this string x, Decimal ErrorValue)
        {
            try
            {
                return Convert.ToDecimal(x);
            }
            catch
            {
                return ErrorValue;
            }
        }

        /// <summary>   A string extension method that converts this object to a date time. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">    string To Convert. </param>
        /// <returns>   The given data converted to a DateTime. </returns>
        public static DateTime ToDateTime(this string x) => Convert.ToDateTime(x);

        /// <summary>   A string extension method that converts this object to a date time. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">            string To Convert. </param>
        /// <param name="ErrorValue">   The error value Date/Time. </param>
        /// <returns>   The given data converted to a DateTime. </returns>
        public static DateTime ToDateTime(this string x, DateTime ErrorValue)
        {
            try
            {
                return Convert.ToDateTime(x);
            }
            catch
            {
                return ErrorValue;
            }
        }

        /// <summary>   A string extension method that escape specifier characters for js. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">    string To Convert. </param>
        /// <returns>   A string. </returns>
        public static string EscapeSpecCharsForJS(this string x) => x.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\r\n", "<br />");

        /// <summary>   A string extension method that null or empty. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">    . </param>
        /// <returns>   true if it succeeds, false if it fails. </returns>
        public static bool NullOrEmpty(this string x) => string.IsNullOrEmpty(x);

        /// <summary>   A string extension method that ensures that int. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">        . </param>
        /// <param name="SetNull">  true to set null. </param>
        /// <returns>   An int? </returns>
        public static int? EnsureInt(this string x, bool SetNull)
        {
            int? nullable;
            try
            {
                nullable = new int?(Convert.ToInt32(x));
            }
            catch
            {
                if (SetNull)
                {
                    return new int?();
                }

                nullable = new int?(0);
            }
            return nullable;
        }

        /// <summary>   A string extension method that splits a string. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">            string To Convert. </param>
        /// <param name="Delimeter">    The delimeter. </param>
        /// <param name="o">            The StringSplitOptions to process. </param>
        /// <returns>   A string[]. </returns>
        public static string[] SplitString(this string x, string Delimeter, StringSplitOptions o) => Regex.Split(x, Regex.Escape(Delimeter));

        /// <summary>   A string extension method that trim start. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">            string To Convert. </param>
        /// <param name="Characters">   The characters. </param>
        /// <returns>   A string. </returns>
        public static string TrimStart(this string x, string Characters) => x.TrimStart(Characters.ToCharArray());

        /// <summary>   A string extension method that trim end. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">            string To Convert. </param>
        /// <param name="Characters">   The characters. </param>
        /// <returns>   A string. </returns>
        public static string TrimEnd(this string x, string Characters) => x.TrimEnd(Characters.ToCharArray());

        /// <summary>   A string extension method that trims. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">            string To Convert. </param>
        /// <param name="Characters">   The characters. </param>
        /// <returns>   A string. </returns>
        public static string Trim(this string x, string Characters) => x.Trim(Characters.ToCharArray());

        /// <summary>   A string extension method that lefts. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">        string To Convert. </param>
        /// <param name="Length">   The length. </param>
        /// <returns>   A string. </returns>
        public static string Left(this string x, int Length) => x.Substring(0, Length);

        /// <summary>   A string extension method that rights. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">        string To Convert. </param>
        /// <param name="Length">   The length. </param>
        /// <returns>   A string. </returns>
        public static string Right(this string x, int Length) => x.Substring(x.Length - Length);

        /// <summary>   A string extension method that converts a DBType to a database type. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="DBType">   The DBType to act on. </param>
        /// <returns>   DBType as a System.Data.DbType. </returns>
        public static DbType ToDbType(this string DBType)
        {
            switch (DBType.ToLower())
            {
                case "bigint":
                    return DbType.Int64;
                case "binary":
                    return DbType.Binary;
                case "bit":
                    return DbType.Boolean;
                case "char":
                    return DbType.AnsiStringFixedLength;
                case "datatable":
                    return DbType.Object;
                case "date":
                    return DbType.Date;
                case "datetime":
                    return DbType.DateTime;
                case "decimal":
                    return DbType.Decimal;
                case "float":
                    return DbType.Double;
                case "guid":
                    return DbType.Guid;
                case "image":
                    return DbType.Object;
                case "int":
                    return DbType.Int32;
                case "money":
                    return DbType.Currency;
                case "nchar":
                    return DbType.StringFixedLength;
                case "ntext":
                    return DbType.String;
                case "numeric":
                    return DbType.Decimal;
                case "nvarchar":
                    return DbType.StringFixedLength;
                case "real":
                    return DbType.Decimal;
                case "smalldatetime":
                    return DbType.DateTime;
                case "smallint":
                    return DbType.Int16;
                case "string":
                    return DbType.String;
                case "sysname":
                    return DbType.String;
                case "text":
                    return DbType.AnsiString;
                case "tinyint":
                    return DbType.Byte;
                case "uniqueidentifier":
                    return DbType.Guid;
                case "varbinary":
                    return DbType.Binary;
                case "varchar":
                    return DbType.AnsiStringFixedLength;
                case "xml":
                    return DbType.String;
                default:
                    return DbType.Object;
            }
        }

        /// <summary>   Turns An XML String Into A Dictionary. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="x">    . </param>
        /// <returns>   x as a Dictionary&lt;string,string&gt; </returns>
        public static Dictionary<string, string> ToDictionaryFromFormattedXML(this string x)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            XPathNavigator navigator = new XPathDocument(new StringReader(x)).CreateNavigator();
            navigator.MoveToChild("root", "");
            navigator.MoveToChild("items", "");
            XPathNodeIterator xpathNodeIterator = navigator.SelectDescendants(XPathNodeType.Element, false);
            while (xpathNodeIterator.MoveNext())
            {
                dictionary.Add(xpathNodeIterator.Current.GetAttribute("key", ""), xpathNodeIterator.Current.GetAttribute("value", ""));
            }

            return dictionary;
        }

        /// <summary>   A string extension method that parse web page name. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        /// <param name="SCRIPTNAME">   The SCRIPTNAME to act on. </param>
        /// <returns>   A string. </returns>
        public static string ParseWeb_Page_Name(this string SCRIPTNAME)
        {
            string[] strArray = SCRIPTNAME.SplitString("/", StringSplitOptions.RemoveEmptyEntries);
            string x = "";
            bool flag = false;
            foreach (string str in strArray)
            {
                if (flag)
                {
                    x = x + str + "/";
                }

                if (str.ToLower().Contains(".com") || str.ToLower().Contains(".org") || str.ToLower().Contains(".net") || str.ToLower().Contains(".info") || str.ToLower().Contains(".us") || str.ToLower().Contains(".gov") || str.ToLower().Contains(".edu") || str.ToLower().Contains(".mil") || str.ToLower().Contains(".int") || str.ToLower().Contains(".info") || str.ToLower().Contains(".us") || str.ToLower().Contains(".gov"))
                {
                    x = "/";
                    flag = true;
                }
            }
            return !flag ? SCRIPTNAME : x.TrimEnd("/");
        }
    }
}
