using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Extensions
{
    public static class Byte
    {
        /// <summary>   The placement byte. </summary>
        internal static byte PlacementByte = 45;

        /// <summary>   A byte extension method that combines. </summary>
        /// <remarks>   Mark Alicz, 12/18/2016. </remarks>
        /// <param name="b1">   The b1 to act on. </param>
        /// <param name="b2">   The second byte. </param>
        /// <returns>   An int. </returns>
        public static int Combine(this byte b1, byte b2) => (int)b1 << 8 | (int)b2;

        /// <summary>   A byte extension method that combine to u short. </summary>
        /// <remarks>   Mark Alicz, 12/18/2016. </remarks>
        /// <param name="b1">   The b1 to act on. </param>
        /// <param name="b2">   The second byte. </param>
        /// <returns>   An ushort. </returns>
        public static ushort CombineToUShort(this byte b1, byte b2) => BitConverter.ToUInt16(new byte[2]
        {
      b2,
      b1
        }, 0);

        /// <summary>   A byte extension method that combine to int 32. </summary>
        /// <remarks>   Mark Alicz, 12/18/2016. </remarks>
        /// <param name="b1">   The b1 to act on. </param>
        /// <param name="b2">   The second byte. </param>
        /// <param name="b3">   The third byte. </param>
        /// <param name="b4">   The fourth byte. </param>
        /// <returns>   An uint. </returns>
        public static uint CombineToInt32(this byte b1, byte b2, byte b3, byte b4) => BitConverter.ToUInt32(new byte[4]
        {
      b4,
      b3,
      b2,
      b1
        }, 0);

        /// <summary>   A byte extension method that combine to int 64. </summary>
        /// <remarks>   Mark Alicz, 12/18/2016. </remarks>
        /// <param name="b1">   The b1 to act on. </param>
        /// <param name="b2">   The second byte. </param>
        /// <param name="b3">   The third byte. </param>
        /// <param name="b4">   The fourth byte. </param>
        /// <param name="b5">   The fifth byte. </param>
        /// <param name="b6">   The b 6. </param>
        /// <param name="b7">   The b 7. </param>
        /// <param name="b8">   The b 8. </param>
        /// <returns>   An ulong. </returns>
        public static ulong CombineToInt64(
          this byte b1,
          byte b2,
          byte b3,
          byte b4,
          byte b5,
          byte b6,
          byte b7,
          byte b8)
        {
            return BitConverter.ToUInt64(new byte[8]
            {
        b8,
        b7,
        b6,
        b5,
        b4,
        b3,
        b2,
        b1
            }, 0);
        }

        /// <summary>   A byte[] extension method that converts an x to a ct string. </summary>
        /// <remarks>   Mark Alicz, 12/18/2016. </remarks>
        /// <param name="x">    . </param>
        /// <returns>   x as a string. </returns>
        public static string ToACTString(this byte[] x)
        {
            string str = "";
            bool flag = false;
            for (int index = 0; index < x.Length; index += 2)
            {
                byte placementByte1 = Byte.PlacementByte;
                byte num;
                byte placementByte2;
                try
                {
                    num = x[index];
                    placementByte2 = x[index + 1];
                }
                catch
                {
                    try
                    {
                        num = x[index];
                        placementByte2 = Byte.PlacementByte;
                        flag = true;
                    }
                    catch
                    {
                        return "Error Calculating ACTString";
                    }
                }
                byte x1 = num.Xor(placementByte2);
                str = str + num.ToBinaryString() + x1.ToBinaryString();
            }
            if (flag)
                str += "PADDING";
            return str;
        }

        public static byte Xor(this byte a, byte b) => (byte)((uint)a ^ (uint)b);

        /// <summary>   From binary string. </summary>
        /// <remarks>   Mark Alicz, 12/18/2016. </remarks>
        /// <exception cref="T:System.Exception">    Thrown when an exception error condition occurs. </exception>
        /// <param name="x">            . </param>
        /// <param name="PadIfNeeded">  (Optional) true if pad if needed. </param>
        /// <returns>   A byte[]. </returns>
        public static byte[] FromBinaryString(string x, bool PadIfNeeded = true)
        {
            string str = x;
            int num = x.Length % 8;
            if (!PadIfNeeded && (uint)num > 0U)
                throw new Exception("Padding Is Off and String is Not divided by 8 evenly");
            if ((uint)num > 0U)
            {
                for (int index = 0; index < num; ++index)
                    str = "0" + str;
            }
            int length = str.Length / 8;
            byte[] numArray = new byte[length];
            for (int index = 0; index < length; ++index)
                numArray[index] = Convert.ToByte(str.Substring(8 * index, 8), 2);
            return numArray;
        }

        /// <summary>   A byte extension method that converts an x to a binary string. </summary>
        /// <remarks>   Mark Alicz, 12/18/2016. </remarks>
        /// <param name="x">    . </param>
        /// <returns>   x as a string. </returns>
        public static string ToBinaryString(this byte x) => Convert.ToString(x, 2).PadLeft(8, '0');

        /// <summary>   A byte[] extension method that drop zero bytes. </summary>
        /// <remarks>   Mark Alicz, 12/18/2016. </remarks>
        /// <param name="x">    . </param>
        /// <returns>   A byte[]. </returns>
        public static byte[] DropZeroBytes(this byte[] x)
        {
            List<byte> byteList = new List<byte>();
            foreach (byte num in x)
            {
                if (num > (byte)0)
                    byteList.Add(num);
            }
            return byteList.ToArray();
        }

        /// <summary>Returns the Percentage Different From The Compare To</summary>
        /// <param name="x"></param>
        /// <param name="CompareTo"></param>
        /// <returns>Decimal Percentage Similarity 100 is Perfect Similar</returns>
        public static Decimal CompareArrays(this byte[] x, byte[] CompareTo)
        {
            int num1 = 0;
            Decimal num2 = 0M;
            Decimal num3 = 100M / (Decimal)x.Length;
            for (int index = 0; index < x.Length && CompareTo.Length >= index; ++index)
            {
                if ((int)x[index] == (int)CompareTo[index])
                    num2 += num3;
                else
                    ++num1;
            }
            return num1 == 0 ? 100M : num2;
        }
    }
}
