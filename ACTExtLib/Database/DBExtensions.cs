using System.Text.RegularExpressions;

namespace ACT.Core.Extensions
{
    public static class DBExtensions
    {
        /// <summary>
        /// Validates the connection string.
        /// </summary>
        /// <param name="ConnectionString">The connection string.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool ValidateConnectionString(string ConnectionString)
        {
            return Regex.IsMatch(ConnectionString, @"^([^=;]+=[^=;]*)(;[^=;]+=[^=;]*)*;?$");
        }
    }
}
