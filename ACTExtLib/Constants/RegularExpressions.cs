namespace ACT.Core.Constants
{
    public static class RegularExpressions
    {
        /// <summary>Hex Color String</summary>
        public static readonly string ColorHex = "^#(?:[0-9a-fA-F]{3}){1,2}$";
        /// <summary>Standard Email Address</summary>
        public static readonly string StandardEmail = "^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$";
        /// <summary>with or without http</summary>
        public static readonly string ValidURL = "(http(s)?://)?([\\w-]+\\.)+[\\w-]+[.com]+(/[/?%&=]*)?";
        /// <summary>Minimum Low Security Password Strength</summary>
        public static readonly string PasswordStrengthA = "^[a-z0-9\\.@#\\$%&]+$";
        /// <summary>Minimum 8 characters at least 1 Alphabet and 1 Number</summary>
        public static readonly string PasswordStrengthB = "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$";
        /// <summary>
        /// Minimum 8 characters at least 1 Uppercase Alphabet, 1 Lowercase Alphabet, 1 Number and 1 Special Character
        /// </summary>
        public static readonly string PasswordStrengthC = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[$@$!%*?&])[A-Za-z\\d$@$!%*?&]{8,}";
        /// <summary>
        /// Minimum 8 and Maximum 10 characters at least 1 Uppercase Alphabet, 1 Lowercase Alphabet, 1 Number and 1 Special Character
        /// </summary>
        public static readonly string PasswordStrengthD = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[$@$!%*?&])[A-Za-z\\d$@$!%*?&]{8,10}";
        /// <summary>
        /// Minimum 16 characters at least 1 Uppercase Alphabet, 1 Lowercase Alphabet, 1 Number and 1 Special Character
        /// </summary>
        public static readonly string SecurePasswordStrength = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[$@$!%*?&])[A-Za-z\\d$@$!%*?&]{16,}";
        /// <summary>without +91 or 0</summary>
        public static readonly string MobilePhoneNumber = "^((\\+){0,1}91(\\s){0,1}(\\-){0,1}(\\s){0,1}){0,1}9[0-9](\\s){0,1}(\\-){0,1}(\\s){0,1}[1-9]{1}[0-9]{7}$";
        /// <summary>Roman Numerals</summary>
        public static readonly string RomanNumerals = "^(?i:(?=[MDCLXVI])((M{0,3})((C[DM])|(D?C{0,3}))?((X[LC])|(L?XX{0,2})|L)?((I[VX])|(V?(II{0,2}))|V)?))$";

        public static readonly string MatchFileType_TEMPLATE = "([a-zA-Z0-9\\s_\\\\.\\-\\(\\):])+(###EXTENSION###)$";
    }
}
