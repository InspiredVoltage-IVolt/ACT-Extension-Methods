using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Extensions
{
   public static class File_Directory_IO_Extensions
   {
      
      public static bool IsBinaryFile(this string path)
      {
         if (path.FileExists() == false) { throw new FileNotFoundException("File: " + path + " Does Not Exist", path); }

         long length = new FileInfo(path).Length;

         if (length == 0) return false;

         using (StreamReader stream = new StreamReader(path))
         {
            int ch;
            while ((ch = stream.Read()) != -1)
            {
               if (isControlChar(ch)) { return true; }
            }
         }
         return false;
      }

      public static bool isControlChar(int ch)
      {
         return (ch > Chars.NUL && ch < Chars.BS) || (ch > Chars.CR && ch < Chars.SUB);
      }

      internal static class Chars
      {
         public static char NUL = (char)0; // Null char
         public static char BS = (char)8; // Back Space
         public static char CR = (char)13; // Carriage Return
         public static char SUB = (char)26; // Substitute
      }
   }
}
