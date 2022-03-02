using ACT.Core.Constants;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;
using System.Security;
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
            else /*if (buffer[0] == 43 && buffer[1] == 47 && buffer[2] == 118)*/
            {
                encoding = System.Text.Encoding.UTF8;
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

        /// <summary>
        /// Determines whether [is image name] [the specified input string].
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns>Returns if the String is a Image Name</returns>
        public static bool FileNameIsImage(this string inputString)
        {
            bool _tmpReturn = false;
            foreach (string fileType in FileFormat_Standards.ImageFileTypes)
            {
                if (inputString.EndsWith(fileType, StringComparison.CurrentCultureIgnoreCase))
                {
                    _tmpReturn = true;
                    return _tmpReturn;
                }
            }

            return _tmpReturn;
        }

        public static string About()
        {
            return "This Contains Basic String Extensions To Make Your Life Easier.";
        }
    }

    public static class String_Extensions_Two
    {

        internal static bool invalid;

        /// <summary>
        /// Extends Basic Regex Domain String Searches.
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        private static string DomainMapper(Match match)
        {
            IdnMapping idnMapping = new IdnMapping();
            string ascii = match.Groups[2].Value;
            try
            {
                ascii = idnMapping.GetAscii(ascii);
            }
            catch (ArgumentException ex)
            {
                invalid = true;
            }
            return match.Groups[1].Value + ascii;
        }

        /// <summary>Parse Out the String</summary>
        /// <param name="DelimitedString"></param>
        /// <param name="ElementPosition"></param>
        /// <param name="Delimiter"></param>
        /// <returns></returns>
        public static string ParseOutString(
          this string DelimitedString,
          int ElementPosition,
          string Delimiter = ",",
          bool Trim = true,
          string ErrorString = "")
        {
            if (Delimiter.NullOrEmpty())
            {
                return ErrorString;
            }

            string[] strArray = DelimitedString.SplitString(Delimiter, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length <= ElementPosition)
            {
                return ErrorString;
            }

            return Trim ? strArray[ElementPosition].Trim() : strArray[ElementPosition].Trim();
        }

        /// <summary>Parse HTTP URL</summary>
        /// <param name="URL"></param>
        /// <returns></returns>
        public static List<string> ParseHTTP_URL(this string URL)
        {
            List<string> stringList = new List<string>();
            if (URL.Contains("http://"))
            {
                stringList.Add("http");
            }
            else if (URL.Contains("https://"))
            {
                stringList.Add("https");
            }
            else
            {
                stringList.Add("unknown");
            }

            string[] strArray = URL.Replace("http://", "").Replace("https://", "").SplitString("/", StringSplitOptions.RemoveEmptyEntries);
            string str1 = strArray[0];
            string str2 = str1.Substring(str1.LastIndexOf("."));
            string str3 = str1.Substring(0, str1.LastIndexOf("."));
            string str4 = str2.Replace(".", "");
            string str5 = "";
            string str6 = !str3.Contains(".") ? str3 : str3.Substring(str3.LastIndexOf(".")).Replace(".", "");
            if (str3.Length > 0)
            {
                str5 = str3.Substring(0, str3.LastIndexOf("."));
            }

            stringList.Add(str5);
            stringList.Add(str6);
            stringList.Add(str4);
            for (int index = 1; index < ((IEnumerable<string>)strArray).Count<string>(); ++index)
            {
                stringList.Add(strArray[index]);
            }

            return stringList;
        }

        public static string AddSpacesToSentence(this string text, bool preserveAcronyms)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            StringBuilder stringBuilder = new StringBuilder(text.Length * 2);
            stringBuilder.Append(text[0]);
            for (int index = 1; index < text.Length; ++index)
            {
                if (char.IsUpper(text[index]) && (text[index - 1] != ' ' && !char.IsUpper(text[index - 1]) || preserveAcronyms && char.IsUpper(text[index - 1]) && index < text.Length - 1 && !char.IsUpper(text[index + 1])))
                {
                    stringBuilder.Append(' ');
                }

                stringBuilder.Append(text[index]);
            }
            return stringBuilder.ToString();
        }

        /// <summary>Compute the distance between two strings.</summary>
        public static int ComputeStringDifference(this string s, string t)
        {
            int length1 = s.Length;
            int length2 = t.Length;
            int[,] numArray = new int[length1 + 1, length2 + 1];
            if (length1 == 0)
            {
                return length2;
            }

            if (length2 == 0)
            {
                return length1;
            }

            int index1 = 0;
            while (index1 <= length1)
            {
                numArray[index1, 0] = index1++;
            }

            int index2 = 0;
            while (index2 <= length2)
            {
                numArray[0, index2] = index2++;
            }

            for (int index3 = 1; index3 <= length1; ++index3)
            {
                for (int index4 = 1; index4 <= length2; ++index4)
                {
                    int num = t[index4 - 1] == s[index3 - 1] ? 0 : 1;
                    numArray[index3, index4] = Math.Min(Math.Min(numArray[index3 - 1, index4] + 1, numArray[index3, index4 - 1] + 1), numArray[index3 - 1, index4 - 1] + num);
                }
            }
            return numArray[length1, length2];
        }

        /// <summary>
        /// Converts a string to a Useless SecureString.
        /// </summary>
        /// <param name="x">string you want to convert</param>
        /// <returns><see cref="System.Security.SecureString"/>SecureString Ugh</returns>
        public static SecureString ToSecureString(this string x)
        {
            SecureString secureString = new SecureString();
            foreach (char c in x)
            {
                secureString.AppendChar(c);
            }

            return secureString;
        }

        /// <summary>Checks the string for a valid Phone Number Format</summary>
        /// <param name="Phone">string to test</param>
        /// <returns>bool:true or false</returns>
        public static bool IsValidPhoneNumber(this string Phone) => new Regex("\\(?\\d{3}\\)?-? *\\d{3}-? *-?\\d{4}").Matches(Phone).Count > 0;

        /// <summary>Checks for a Valid Email</summary>
        /// <param name="strIn">string to test</param>
        /// <returns>bool:true or false</returns>
        public static bool IsValidEmail(this string strIn)
        {
            invalid = false;
            if (string.IsNullOrEmpty(strIn))
            {
                return false;
            }

            strIn = Regex.Replace(strIn, "(@)(.+)$", new MatchEvaluator(DomainMapper));
            return !invalid && Regex.IsMatch(strIn, "^(?(\")(\"[^\"]+?\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9]{2,17}))$", RegexOptions.IgnoreCase);
        }

        /// <summary>Checks for Password Complexity</summary>
        /// <param name="pwd">Self Ref Password To check</param>
        /// <param name="minLength">Default 8</param>
        /// <param name="numUpper">Upper Case Count - Default 1</param>
        /// <param name="numLower">Lower Case Count - Default 1</param>
        /// <param name="numNumbers">Number Count - Default 0</param>
        /// <param name="numSpecial">Special Character Count - Default 1</param>
        /// <returns>bool:true or false</returns>
        public static bool IsComplex(this string pwd, int minLength = 8, int numUpper = 1, int numLower = 1, int numNumbers = 0, int numSpecial = 1)
        {
            Regex regex1 = new Regex("[A-Z]");
            Regex regex2 = new Regex("[a-z]");
            Regex regex3 = new Regex("[0-9]");
            Regex regex4 = new Regex("[^a-zA-Z0-9]");
            return pwd.Length >= minLength && regex1.Matches(pwd).Count >= numUpper && regex2.Matches(pwd).Count >= numLower && regex3.Matches(pwd).Count >= numNumbers && regex4.Matches(pwd).Count >= numSpecial;
        }

        /// <summary>
        ///     Converts a string to an escaped JavaString string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>The JS string.</returns>
        public static string ToJsString(this string str)
        {
            if (!str.IsSet()) { return str; }

            str = str.Replace("\\", "\\\\");
            str = str.Replace("'", "\\'");
            str = str.Replace("\r", "\\r");
            str = str.Replace("\n", "\\n");
            str = str.Replace("\"", "\\\"");
            return str;
        }

        /// <summary>
        ///     Function to check a max word length, used i.e. in topic names.
        /// </summary>
        /// <param name="text">The raw string to format</param>
        /// <param name="maxWordLength">The max Word Length.</param>
        /// <returns>The formatted string</returns>
        public static bool AreAnyWordsOverMaxLength(this string text, int maxWordLength)
        {
            if (maxWordLength <= 0 || text.Length <= 0)
            {
                return false;
            }

            return ((IEnumerable<string>)text.Split(' ')).Where<string>(w => w.IsSet() && w.Length > maxWordLength).Any<string>();
        }

        /// <summary>
        ///     Function to remove words in a string based on a max string length, used i.e. in search.
        /// </summary>
        /// <param name="text">The raw string to format</param>
        /// <param name="maxStringLength">The max string length.</param>
        /// <returns>The formatted string</returns>
        public static string TrimWordsOverMaxLengthWordsPreserved(this string text, int maxStringLength)
        {
            string str1 = string.Empty;
            if (maxStringLength <= 0 || text.Length <= 0)
            {
                return str1.Trim();
            }

            string[] strArray = text.Trim().Split(' ');
            int num1 = 0;
            int num2 = 0;
            foreach (string str2 in strArray)
            {
                num1 += str2.Length;
                if (num1 > maxStringLength)
                {
                    if (num2 == 0)
                    {
                        str1 = string.Empty;
                        break;
                    }
                    break;
                }
                ++num2;
                str1 = str1 + " " + str2;
            }
            return str1.Trim();
        }

        /// <summary>Fast index of.</summary>
        /// <param name="source">The source.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>The fast index of.</returns>
        public static int FastIndexOf(this string source, string pattern)
        {
            if (pattern.Length == 0)
            {
                return 0;
            }

            if (pattern.Length == 1)
            {
                return source.IndexOf(pattern[0]);
            }

            int count = source.Length - pattern.Length + 1;
            if (count < 1)
            {
                return -1;
            }

            char ch1 = pattern[0];
            char ch2 = pattern[1];
            int num1 = source.IndexOf(ch1, 0, count);
            while (num1 != -1)
            {
                if (source[num1 + 1] != ch2)
                {
                    int num2;
                    num1 = source.IndexOf(ch1, num2 = num1 + 1, count - num2);
                }
                else
                {
                    bool flag = true;
                    for (int index = 2; index < pattern.Length; ++index)
                    {
                        if (source[num1 + index] != pattern[index])
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        return num1;
                    }

                    int num3;
                    num1 = source.IndexOf(ch1, num3 = num1 + 1, count - num3);
                }
            }
            return -1;
        }

        /// <summary>
        ///     Does an action for each character in the input string. Kind of useless, but in a
        ///     useful way. ;)
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="forEachAction">For each action.</param>
        public static void ForEachChar(this string input, Action<char> forEachAction)
        {
            foreach (char ch in input)
            {
                forEachAction(ch);
            }
        }

        /// <summary>Formats a string with the provided parameters</summary>
        /// <param name="s">The s.</param>
        /// <param name="args">The args.</param>
        /// <returns>The formatted string</returns>
        public static string FormatWith(this string s, params object[] args) => s.IsNotSet() ? null : string.Format(s, args);

        /// <summary>Removes empty strings from the list</summary>
        /// <param name="inputList">The input list.</param>
        /// <returns></returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="inputList" /> is <c>null</c>.</exception>

        public static List<string> GetNewNoEmptyStrings(this IEnumerable<string> inputList) => inputList.Where<string>(x => x.IsSet()).ToList<string>();

        /// <summary>
        ///     Removes strings that are smaller then <paramref name="minSize" />
        /// </summary>
        /// <param name="inputList">The input list.</param>
        /// <param name="minSize">The minimum size.</param>
        /// <returns></returns>

        public static List<string> GetNewNoSmallStrings(
           this IEnumerable<string> inputList,
          int minSize)
        {
            return inputList.Where<string>(x => x.Length >= minSize).ToList<string>();
        }

        /// <summary>
        ///     When the string is trimmed, is it <see langword="null" /> or empty?
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns>
        ///     The is <see langword="null" /> or empty trimmed.
        /// </returns>
        public static bool IsNotSet(this string inputString) => string.IsNullOrWhiteSpace(inputString);

        /// <summary>
        ///     When the string is trimmed, is it <see langword="null" /> or empty?
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns>
        ///     The is <see langword="null" /> or empty trimmed.
        /// </returns>
        public static bool IsSet(this string inputString) => !string.IsNullOrWhiteSpace(inputString);

        /// <summary>
        ///     Removes multiple single quote ' characters from a string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The remove multiple single quotes.</returns>
        public static string RemoveMultipleSingleQuotes(this string text)
        {
            string empty = string.Empty;
            return text.IsNotSet() ? empty : new Regex("\\'").Replace(text, "'");
        }

        /// <summary>
        ///     Removes multiple whitespace characters from a string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The remove multiple whitespace.</returns>
        public static string RemoveMultipleWhitespace(this string text)
        {
            string empty = string.Empty;
            return text.IsNotSet() ? empty : new Regex("\\s+").Replace(text, " ");
        }

        /// <summary>
        ///     Converts a string into it's hexadecimal representation.
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns>The string to hex bytes.</returns>
        public static string StringToHexBytes(this string inputString)
        {
            string empty = string.Empty;
            if (inputString.IsNotSet()) { return empty; }

            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte num in inputString.ToBytes())
            {
                stringBuilder.Append(num.ToString("x2").ToLower());
            }

            return stringBuilder.ToString();
        }

        /// <summary>Converts a string to a list using delimiter.</summary>
        /// <param name="str">starting string</param>
        /// <param name="delimiter">value that delineates the string</param>
        /// <returns>list of strings</returns>
        public static List<string> StringToList(this string str, char delimiter) => str.StringToList(delimiter, new List<string>());

        /// <summary>Converts a string to a list using delimiter.</summary>
        /// <param name="str">starting string</param>
        /// <param name="delimiter">value that delineates the string</param>
        /// <param name="exclude">items to exclude from list</param>
        /// <returns>list of strings</returns>

        public static List<string> StringToList(
           this string str,
          char delimiter,
           List<string> exclude)
        {
            List<string> list = ((IEnumerable<string>)str.Split(delimiter)).ToList<string>();
            list.RemoveAll(new Predicate<string>(exclude.Contains));
            list.Remove(delimiter.ToString());
            return list;
        }

        /// <summary>
        ///     Creates a delimited string an enumerable list of T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objList">The object list.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>The list to string.</returns>
        /// <exception cref="T:System.ArgumentNullException">objList;objList is null.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="objList" /> is <c>null</c>.</exception>
        public static string ToDelimitedString<T>(this IEnumerable<T> objList, string delimiter) where T : IConvertible
        {
            if (objList == null)
            {
                throw new ArgumentNullException(nameof(objList), "objList is null.");
            }

            StringBuilder sb = new StringBuilder();
            objList.ForEachFirst<T>((x, isFirst) =>
            {
                if (!isFirst)
                {
                    sb.Append(delimiter);
                }

                sb.Append(x);
            });
            return sb.ToString();
        }

        public static string ToJsonArray(this List<string> objList)
        {
            string x = "[ ";
            foreach (string str in objList)
            {
                x = x + "\"" + str + "\", ";
            }

            return x.TrimEnd(",") + "]";
        }

        /// <summary>
        ///     Cleans a string into a proper RegEx statement.
        ///     E.g. "[b]Whatever[/b]" will be converted to:
        ///     "\[b\]Whatever\[\/b\]"
        /// </summary>
        /// <param name="input">
        /// </param>
        /// <returns>The to reg ex string.</returns>

        public static string ToRegExString(this string input)
        {
            StringBuilder sb = new StringBuilder();
            input.ForEachChar(c =>
            {
                if (!char.IsWhiteSpace(c) && !char.IsLetterOrDigit(c) && c != '_')
                {
                    sb.Append("\\");
                }

                sb.Append(c);
            });
            return sb.ToString();
        }

        public static bool CheckPasswordLength(this string Password)
        {
            int num;
            switch (Password)
            {
                case "":
                case null:
                    num = 1;
                    break;
                default:
                    num = Password.Length < 3 ? 1 : 0;
                    break;
            }
            return num == 0;
        }

        /// <summary>
        ///     Truncates a string with the specified limits and adds (...) to the end if truncated
        /// </summary>
        /// <param name="input">input string</param>
        /// <param name="limit">max size of string</param>
        /// <returns>truncated string</returns>
        public static string Truncate(this string input, int inputLimit, string cutOfString = "...")
        {
            string str1 = input;
            if (input.IsNotSet())
            {
                return null;
            }

            int length1 = inputLimit - cutOfString.Length;
            if (str1.Length > length1 && length1 > 0)
            {
                string str2 = str1.Substring(0, length1);
                if (input.Substring(str2.Length, 1) != " ")
                {
                    int length2 = str2.LastIndexOf(" ");
                    if (length2 != -1)
                    {
                        str2 = str2.Substring(0, length2);
                    }
                }
                str1 = str2 + cutOfString;
            }
            return str1;
        }

        /// <summary>
        ///     Truncates a string with the specified limits by adding (...) to the middle
        /// </summary>
        /// <param name="input">input string</param>
        /// <param name="limit">max size of string</param>
        /// <returns>truncated string</returns>
        public static string TruncateMiddle(this string input, int limit)
        {
            if (input.IsNotSet())
            {
                return null;
            }

            string str = input;
            if (str.Length > limit && limit > 0)
            {
                int length1 = limit / 2 - "...".Length / 2;
                int length2 = limit - length1 - "...".Length / 2;
                if (length1 + length2 + "...".Length < limit)
                {
                    ++length2;
                }
                else if (length1 + length2 + "...".Length > limit)
                {
                    --length2;
                }

                str = input.Substring(0, length1) + "..." + input.Substring(input.Length - length2, length2);
            }
            return str;
        }

        /// <summary>Convert a input string to a byte array</summary>
        /// <param name="value">Input string.</param>
        /// <returns>The Byte String</returns>
        public static byte[] ToBytes(this string value) => Encoding.UTF8.GetBytes(value);

        public static byte[] ToByteArrayFromBinaryString(this string x) => Byte.FromBinaryString(x);

        /// <summary>
        ///     A string extension method that initializes this object from the given from a ct string.
        /// </summary>
        /// <remarks>   Mark Alicz, 12/18/2016. </remarks>
        /// <param name="x">    . </param>
        /// <returns>   A byte[]. </returns>
        public static byte[] FromACTString(this string x)
        {
            bool flag = false;
            string str = x;
            if (x.EndsWith("HASPADDING"))
            {
                flag = true;
                str.Substring(0, str.Length - 10);
            }
            byte[] fromBinaryString = x.ToByteArrayFromBinaryString();
            List<byte> byteList = new List<byte>();
            for (int index = 0; index < fromBinaryString.Length; index += 2)
            {
                byte a = fromBinaryString[index];
                byte b = fromBinaryString[index];
                byte num = a.Xor(b);
                byteList.Add(a);
                byteList.Add(num);
            }
            if (flag)
            {
                byteList.RemoveAt(byteList.Count - 1);
            }

            return byteList.ToArray();
        }

        /// <summary>Generates The Stream From a String</summary>
        /// <param name="s">String To Convert To A Stream</param>
        /// <returns>Stream</returns>
        public static Stream GenerateStreamFromString(this string s)
        {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream);
            streamWriter.Write(s);
            streamWriter.Flush();
            memoryStream.Position = 0L;
            return memoryStream;
        }

    }
}
