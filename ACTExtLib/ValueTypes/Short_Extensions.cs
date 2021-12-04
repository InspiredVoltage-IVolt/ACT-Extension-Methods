namespace ACT.Core.Extensions
{
    public static class Short
    {
        public static byte[] ToByteArray(this ushort x) => new byte[2]
        {
      (byte) ((uint) x >> 8),
      (byte) ( x & (uint) byte.MaxValue)
        };
    }
}
