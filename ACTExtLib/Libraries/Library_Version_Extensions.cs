using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Extensions.Libraries
{

    public static class Library_Version_Extensions
    {
        public static uint VERSION2PART = 19999;
        public static uint VERSION3PART = 1999999;
        public static uint VERSION4PART = 199999999;

        /// <summary>
        /// Following ACT Design OF Adding a 1 then Eact Part Must Have a ZERO THEN Value Or Value if > 9
        /// i.e  Version 1.1.1.33 = 101010133
        /// </summary>
        /// <param name="VersionString">Following The ACT Design OF 1xxxxxxxx</param>
        /// <param name="PartCount">Part Count Of Version 2-4</param>
        /// <returns></returns>
        public static uint VersionStringToInt(this string VersionString, [Range(2, 4)] uint PartCount)
        {
            string _TmpReturn = VersionString.Replace(" ", "");

            try
            {
                string[] Parts = new string[4];
                string[] _Data = _TmpReturn.Split('.', StringSplitOptions.RemoveEmptyEntries);

                if (_Data.Count() != 4) { return 0; }

                if (PartCount >= 2)
                {
                    if (_Data[0].Length == 1) { Parts[0] = "0" + _Data[0]; } else { Parts[0] = _Data[0]; }
                    if (_Data[1].Length == 1) { Parts[1] = "0" + _Data[1]; } else { Parts[1] = _Data[1]; }
                }

                if (PartCount == 3)
                {
                    if (_Data[2].Length == 1) { Parts[2] = "0" + _Data[2]; } else { Parts[2] = _Data[2]; }
                }

                if (PartCount == 4)
                {
                    if (_Data[3].Length == 1) { Parts[3] = "0" + _Data[3]; } else { Parts[3] = _Data[3]; }
                }

                _TmpReturn = "1";
                for (int p = 0; p < PartCount; p++) { _TmpReturn += Parts[p]; }
                return Convert.ToUInt32(_TmpReturn);
            }
            catch                // Log Error
            {
                return 0;
            }

        }

        /// <summary>
        /// Converts an Integer that ACT_Designed Version Int
        /// </summary>
        /// <param name="ACT_Designed_VersionInt">Following The ACT Design OF 1xxxxxxxx</param>
        /// <param name="PartCount">2-4</param>
        /// <returns>Returns A String</returns>
        /// <exception cref="Exception"></exception>
        public static string VersionIntToString(this uint ACT_Designed_VersionInt, [Range(2, 4)] uint PartCount)
        {
            uint _Val = ACT_Designed_VersionInt;
            string _Data = _Val.ToString();

            if (PartCount == 4) { if (_Val <= VERSION4PART || _Data.Length != VERSION4PART.ToString().Length) { throw new Exception("The Integer Must Be MAX of" + VERSION4PART.ToString() + " AND MIN of" + VERSION4PART.ToString().Replace("9", "0")); } }
            else if (PartCount == 3) { if (_Val <= VERSION3PART || _Data.Length != VERSION3PART.ToString().Length) { throw new Exception("The Integer Must Be MAX of" + VERSION3PART.ToString() + " AND MIN of" + VERSION3PART.ToString().Replace("9", "0")); } }
            else if (PartCount == 2) { if (_Val <= VERSION2PART || _Data.Length != VERSION2PART.ToString().Length) { throw new Exception("The Integer Must Be MAX of" + VERSION3PART.ToString() + " AND MIN of" + VERSION3PART.ToString().Replace("9", "0")); } }

            if (PartCount == 2)
            {
                string[] Parts = new string[4];
                Parts[0] = _Data[1].ToString() + _Data[2].ToString();
                Parts[1] = _Data[3].ToString() + _Data[4].ToString();
                return Parts[0] + Parts[1];
            }
            else if (PartCount == 3)
            {
                string[] Parts = new string[4];
                Parts[0] = _Data[1].ToString() + _Data[2].ToString();
                Parts[1] = _Data[3].ToString() + _Data[4].ToString();
                Parts[2] = _Data[5].ToString() + _Data[6].ToString();
                return Parts[0] + Parts[1] + Parts[2];
            }
            else
            {
                string[] Parts = new string[4];
                Parts[0] = _Data[1].ToString() + _Data[2].ToString();
                Parts[1] = _Data[3].ToString() + _Data[4].ToString();
                Parts[2] = _Data[5].ToString() + _Data[6].ToString();
                Parts[3] = _Data[7].ToString() + _Data[8].ToString();
                return Parts[0] + Parts[1] + Parts[2] + Parts[3];
            }
        }
    }
}
