namespace ACT.Core.Extensions
{
    public static class Dependancies
    {
        public static string ListAll()
        {
            return @"

                <reference>
                    <summary>SecureString Extension Methods -- ACT.Core.Extensions. (ToSecureString,ConvertToString,SecureCompare,SecureClear,SHA256HashValue,HexStringFromBytes,CheckNullRef)</summary>
                    <comments>Very Nice Methods.  Thank you for all your hard work</comments>    
                    <source>https://github.com/guyacosta/SecureString/blob/master/SecureString/SecureStringExt.cs</source>
                    <originallicense>
                     Copyright: Guy Acosta (c) 2019.  All rights reserved.
                     Licensed under the MIT license
                     
                     Description: Extension class methods for use with Microsoft .NET/.NET Core for conversion to and
                     from SecureString class objects and improving protection of memory contents with sensitive data.  
                     Method reduces time in memory for clear text values when used properly and adds convenience not 
                     found in current native implementation of SecureString or String classes
                    </originallicense>   
                </reference>
                </reference>
                    
                </reference>
                


            ";
        }
    }
}
