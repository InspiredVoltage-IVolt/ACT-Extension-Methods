namespace ACT.Core.Extensions.ReferenceTypes
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;

    namespace ACT.Core.Extensions
    {
        public static class SecureString_Extensions
        {
            /// <summary>
            /// Squeeze the bytes out of a securestring
            /// </summary>
            /// <param name="x"><see cref="SecureString"/></param>
            /// <returns>byte[]</returns>
            public static byte[] GetBytes(this SecureString x)
            {
                byte[] numArray1 = new byte[x.Length * 2];
                IntPtr num = IntPtr.Zero;
                try
                {
                    int ofs = 0;
                    num = Marshal.SecureStringToGlobalAllocUnicode(x);
                    for (int index1 = 0; index1 < x.Length; ++index1)
                    {
                        byte[] numArray2 = new byte[2]
                        {
            Marshal.ReadByte(num, ofs),
            Marshal.ReadByte(num, ofs + 1)
                        };
                        numArray1[ofs] = numArray2[0];
                        int index2 = ofs + 1;
                        numArray1[index2] = numArray2[1];
                        ofs = index2 + 1;
                    }
                }
                finally
                {
                    Marshal.ZeroFreeGlobalAllocUnicode(num);
                }
                return numArray1;
            }

            /// <summary>
            /// A SecureString extension method that converts this object to a byte array.
            /// </summary>
            /// <remarks>   Mark Alicz, 12/17/2016. </remarks>
            /// <exception cref="T:System.ArgumentNullException">    Thrown when one or more required arguments are
            /// null. </exception>
            /// <param name="secureString"> The secureString to act on. </param>
            /// <param name="encoding">     (Optional) the encoding. </param>
            /// <returns>   The given data converted to a byte[]. </returns>
            public static byte[] ToByteArray(this SecureString secureString, Encoding encoding = null)
            {
                if (secureString == null)
                {
                    throw new ArgumentNullException(nameof(secureString));
                }

                encoding = encoding ?? Encoding.UTF8;
                IntPtr num = IntPtr.Zero;
                try
                {
                    num = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                    return encoding.GetBytes(Marshal.PtrToStringUni(num));
                }
                finally
                {
                    if (num != IntPtr.Zero)
                    {
                        Marshal.ZeroFreeBSTR(num);
                    }
                }
            }

            /// <summary>
            /// A SecureString extension method that converts a securePassword to an unsecure string.
            /// </summary>
            /// <remarks>   Mark Alicz, 12/17/2016. </remarks>
            /// <exception cref="T:System.ArgumentNullException">    Thrown when one or more required arguments are
            /// null. </exception>
            /// <param name="securePassword">   The securePassword to act on. </param>
            /// <returns>   The given data converted to an unsecure string. </returns>
            public static string ConvertToUnsecureString(this SecureString securePassword)
            {
                if (securePassword == null)
                {
                    throw new ArgumentNullException(nameof(securePassword));
                }

                IntPtr num = IntPtr.Zero;
                try
                {
                    num = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                    return Marshal.PtrToStringUni(num);
                }
                finally
                {
                    Marshal.ZeroFreeGlobalAllocUnicode(num);
                }
            }
        }
    }
}
