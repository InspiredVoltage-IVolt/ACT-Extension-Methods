namespace ACT.Core.Extensions.Constants
{
    public static class International_Standards
    {

        /// <summary>
        /// The set of characters that are unreserved in RFC 2396 but are NOT unreserved in RFC 3986.
        /// </summary>
        public static readonly string[] UriRfc3986CharsToEscape = new string[5]
        {
            "!",
            "*",
            "'",
            "(",
            ")"
        };
    }
}
