namespace ACT.Core.Extensions
{
    public static class WindowsEnums_Extensions
    {
        /// <summary>
        /// Convert String To Windows Console Color
        /// </summary>
        /// <param name="ConsoleColorValue"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static ConsoleColor ToWindowsConsoleColor(this string ConsoleColorValue)
        {
            try { return (ConsoleColor)Enum.Parse(typeof(ConsoleColor), ConsoleColorValue); }
            catch { throw new Exception(ConsoleColorValue + " Is Not A Valid Console Color."); }
        }

        /// <summary>
        /// Windows ConsoleColor ToString
        /// </summary>
        /// <param name="ConsoleColorValue"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string ToString(this ConsoleColor ConsoleColorValue)
        {
            try { return ConsoleColorValue.ToString(); }
            catch { throw new Exception("Unknown Console Color."); }
        }
    }
}
