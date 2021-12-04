namespace ACT.Core.Extensions.ValueTypes
{
    public static class Integer
    {
        /// <summary>Bit Positions (Factors of 2)</summary>
        public static int[] BitPositions = new int[31]
        {
      1,
      2,
      4,
      8,
      16,
      32,
      64,
      128,
      256,
      512,
      1024,
      2048,
      4096,
      8192,
      16384,
      32768,
      65536,
      131072,
      262144,
      524288,
      1048576,
      2097152,
      4194304,
      8388608,
      16777216,
      33554432,
      67108864,
      134217728,
      268435456,
      536870912,
      1073741824
        };

        /// <summary>   An int extension method that query if 'x' is single bit. </summary>
        /// <remarks>   Mark Alicz, 7/10/2016. </remarks>
        /// <param name="x">    The x to act on. </param>
        /// <returns>   true if single bit, false if not. </returns>
        public static bool IsSingleBit(this int x) => ((IEnumerable<int>)Integer.BitPositions).Contains<int>(x);

        /// <summary>   An int extension method that gets the previous power of two. </summary>
        /// <remarks>   Mark Alicz, 7/10/2016. </remarks>
        /// <param name="x">    The x to act on. </param>
        /// <returns>   The previous power of two. </returns>
        public static int GetPreviousPowerOfTwo(this int x) => ((IEnumerable<int>)Integer.BitPositions).Where<int>(xx => xx < x).Max();

        /// <summary>   An int extension method that gets the next power of two. </summary>
        /// <remarks>   Mark Alicz, 7/10/2016. </remarks>
        /// <param name="x">    The x to act on. </param>
        /// <returns>   The next power of two. </returns>
        public static int GetNextPowerOfTwo(this int x) => ((IEnumerable<int>)Integer.BitPositions).Where<int>(xx => xx > x).Min();

        /// <summary>   An int extension method that query if 'x' is power of two. </summary>
        /// <remarks>   Mark Alicz, 7/10/2016. </remarks>
        /// <param name="x">    The x to act on. </param>
        /// <returns>   true if power of two, false if not. </returns>
        public static bool IsPowerOfTwo(this int x) => ((IEnumerable<int>)Integer.BitPositions).Contains<int>(x);

        /// <summary>   An int extension method that gets excel column name. A, B, AA, ZZ Etc. </summary>
        /// <remarks>   Mark Alicz, 7/10/2016. </remarks>
        /// <param name="columnNumber"> The columnNumber to act on. </param>
        /// <returns>   The excel column name. </returns>
        public static string GetExcelColumnName(this int columnNumber)
        {
            int num1 = columnNumber;
            string str = string.Empty;
            int num2;
            for (; num1 > 0; num1 = (num1 - num2) / 26)
            {
                num2 = (num1 - 1) % 26;
                str = Convert.ToChar(65 + num2).ToString() + str;
            }
            return str;
        }

        /// <summary>To File Size String</summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToFileSize(this int source) => Convert.ToInt64(source).ToFileSize();

        /// <summary>To File Size String</summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToFileSize(this long source)
        {
            double num = Convert.ToDouble(source);
            if (num >= Math.Pow(1024.0, 3.0))
            {
                return Math.Round(num / Math.Pow(1024.0, 3.0), 2).ToString() + " GB";
            }

            if (num >= Math.Pow(1024.0, 2.0))
            {
                return Math.Round(num / Math.Pow(1024.0, 2.0), 2).ToString() + " MB";
            }

            return num >= 1024.0 ? Math.Round(num / 1024.0, 2).ToString() + " KB" : num.ToString() + " Bytes";
        }
    }

    public static class UnsignedInteger
    {
        /// <summary>Convert to Binary String</summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static string ToBinary(this uint x)
        {
            byte[] bytes = BitConverter.GetBytes(x);
            string str = "";
            foreach (byte x1 in ((IEnumerable<byte>)bytes).Reverse<byte>())
            {
                str += x1.ToBinaryString();
            }

            return str;
        }

        /// <summary>
        /// Converts a <code>uint</code> value to a <code>byte[]</code>.
        /// </summary>
        /// <param name="value">The <code>uint</code> value to convert.</param>
        /// <returns>A <code>byte[]</code> representing the <code>uint</code> value.</returns>
        public static byte[] ToByteArray(this uint value)
        {
            int length = 4;
            byte[] numArray = new byte[length];
            for (int index = 0; index < length; ++index)
            {
                int num = (length - (index + 1)) * 8;
                numArray[index] = (byte)((value & (ulong)((long)byte.MaxValue << num)) >> num);
            }
            return numArray;
        }
    }
}
