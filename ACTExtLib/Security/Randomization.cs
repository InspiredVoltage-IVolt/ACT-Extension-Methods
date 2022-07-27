using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Constants;

namespace ACT.Core.Extensions.Security
{
    /// <summary>
    /// Randomization Class
    /// </summary>
    public static class Randomization
    {
        internal static string GlobalConstantAllowedCharacter = Constants.Common_Constants.Randomization_AllowedCharacters;

        public static string GenerateString(int length)
        {
            var bytes = new byte[length];

            using (var random = RandomNumberGenerator.Create()) { random.GetBytes(bytes); }

            return new string(bytes.Select(x => GlobalConstantAllowedCharacter[x % GlobalConstantAllowedCharacter.Length]).ToArray());
        }

        public static string GenerateString(int MinLength, int MaxLength)
        {            
            var _tmpReturn = GenerateString(MaxLength);

            int _ReturnSize = Random.Shared.Next(MinLength, MaxLength);

            return _tmpReturn.Substring(0, _ReturnSize);
        }

        public static string GenerateString(int MinLength, int MaxLength,string SpecificCharacters)
        {
            GlobalConstantAllowedCharacter = SpecificCharacters;
            var _tmpReturn = GenerateString(MaxLength);
            GlobalConstantAllowedCharacter = Constants.Common_Constants.Randomization_AllowedCharacters;

            int _ReturnSize = Random.Shared.Next(MinLength, MaxLength);
            return _tmpReturn.Substring(0, _ReturnSize);
        }

        public static int RandomNumber(int Min, int Max)
        {
            return Random.Shared.Next(Min, Max);
        }

    }
}
