using System.Text;
using System.Text.RegularExpressions;

namespace ACT.Core.Extensions
{
    public static class ACTRomanNumeralExtensions
    {
        public static int NumberOfRomanNumeralMaps = 13;
        public static readonly Dictionary<string, int> romanNumerals = new Dictionary<string, int>(NumberOfRomanNumeralMaps)
                {
      {
        "M",
        1000
      },
      {
        "CM",
        900
      },
      {
        "D",
        500
      },
      {
        "CD",
        400
      },
      {
        "C",
        100
      },
      {
        "XC",
        90
      },
      {
        "L",
        50
      },
      {
        "XL",
        40
      },
      {
        "X",
        10
      },
      {
        "IX",
        9
      },
      {
        "V",
        5
      },
      {
        "IV",
        4
      },
      {
        "I",
        1
      }
    };
        public static readonly Regex validRomanNumeral = new Regex("^(?i:(?=[MDCLXVI])((M{0,3})((C[DM])|(D?C{0,3}))?((X[LC])|(L?XX{0,2})|L)?((I[VX])|(V?(II{0,2}))|V)?))$", RegexOptions.Compiled);

        public static bool IsValidRomanNumeral(this string value) => validRomanNumeral.IsMatch(value);

        public static int ParseRomanNumeral(this string value)
        {
            value = value != null ? value.ToUpperInvariant().Trim() : throw new ArgumentNullException(nameof(value));
            int length = value.Length;
            if (length == 0 || !value.IsValidRomanNumeral())
            {
                throw new ArgumentException("Empty or invalid Roman numeral string.", nameof(value));
            }

            int num1 = 0;
            int num2 = length;
            while (num2 > 0)
            {
                Dictionary<string, int> romanNumerals1 = romanNumerals;
                char ch = value[--num2];
                string key1 = ch.ToString();
                int num3 = romanNumerals1[key1];
                if (num2 > 0)
                {
                    Dictionary<string, int> romanNumerals2 = romanNumerals;
                    ch = value[num2 - 1];
                    string key2 = ch.ToString();
                    int num4 = romanNumerals2[key2];
                    if (num4 < num3)
                    {
                        num3 -= num4;
                        --num2;
                    }
                }
                num1 += num3;
            }
            return num1;
        }

        public static string ToRomanNumeral(this int x)
        {
            if (x < 1 || x > 3999)
            {
                throw new ArgumentOutOfRangeException("value", x, "Argument out of Roman numeral range.");
            }

            StringBuilder stringBuilder = new StringBuilder(15);
            foreach (KeyValuePair<string, int> romanNumeral in romanNumerals)
            {
                for (; x / romanNumeral.Value > 0; x -= romanNumeral.Value)
                {
                    stringBuilder.Append(romanNumeral.Key);
                }
            }
            return stringBuilder.ToString();
        }
    }
}
