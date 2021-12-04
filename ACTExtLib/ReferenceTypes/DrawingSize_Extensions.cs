using System.Drawing;

namespace ACT.Core.Extensions
{
    public static class SystemDrawingSizeExtensions
    {
        /// <summary>
        /// Returns a Size Object From A Formatted String Object
        /// String Format Is {Width=100,Height=100}
        /// </summary>
        /// <param name="x">SizeObject</param>
        /// <param name="Value">String</param>
        /// <returns></returns>
        public static Size FromString(this Size x, string Value)
        {
            Value = Value.Replace("{Width=", "").Replace("Height=", "").Replace("}", "");
            string[] strArray = Value.Split(",".ToCharArray());
            return new Size(Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]));
        }
    }
}
