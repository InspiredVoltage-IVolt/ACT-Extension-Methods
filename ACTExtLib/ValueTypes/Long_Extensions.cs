namespace ACT.Core.Extensions
{
    public static class LongExtensions
    {
        public static readonly string[] SizeSuffixes = new string[9]
        {
      "bytes",
      "KB",
      "MB",
      "GB",
      "TB",
      "PB",
      "EB",
      "ZB",
      "YB"
        };

        public static string GenerateReadableSizeSuffix(this long value)
        {
            if (value < 0L)
            {
                return "-" + (-value).GenerateReadableSizeSuffix();
            }

            if (value == 0L)
            {
                return "0.0 bytes";
            }

            int index = (int)Math.Log(value, 1024.0);
            return string.Format("{0:n1} {1}", value / (Decimal)(1L << index * 10), LongExtensions.SizeSuffixes[index]);
        }
    }
    public static class UnsignedLongExtensions
    {
        /// <summary>Convert to Binary String</summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static string ToBinary(this ulong x)
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
        /// Converts a <code>ulong</code> value to a <code>byte[]</code>.
        /// </summary>
        /// <param name="value">The <code>ulong</code> value to convert.</param>
        /// <returns>A <code>byte[]</code> representing the <code>ulong</code> value.</returns>
        public static byte[] ToByteArray(this ulong value)
        {
            int length = 8;
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
