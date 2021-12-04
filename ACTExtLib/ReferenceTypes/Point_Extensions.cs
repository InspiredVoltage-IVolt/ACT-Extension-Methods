using System.Drawing;

namespace ACT.Core.Extensions
{
    public static class SystemDrawingPointExtensions
    {
        public static Point FromString(this Point x, string Value)
        {
            Value = Value.Replace("{X=", "").Replace("Y=", "").Replace("}", "");
            string[] strArray = Value.Split(",".ToCharArray());
            return new Point(Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]));
        }
    }
}
