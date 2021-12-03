using System.Data;
using System.Data.SqlClient;

namespace ACT.Core.Extensions.ReferenceTypes
{
    public static class DatabaseType_Extensions
    {
        /// <summary>
        /// Convert a Type to A SQLDataType otherwise throw an exception.
        /// </summary>
        /// <param name="x">Type</param>
        /// <param name="IncludeBrackets">Include the brackets around the datatype</param>
        /// <returns>a String</returns>
        /// <exception cref="Exception"></exception>
        public static string ToMSSQLTypeString(this Type x, bool IncludeBrackets = false)
        {
            string str1 = "";
            string str2 = "";
            if (x == typeof(int))
            {
                str1 = "int";
            }
            else if (x == typeof(DateTime))
            {
                str1 = "datetime";
            }
            else if (x == typeof(string))
            {
                str1 = "nvarchar";
                str2 = "(MAX)";
            }
            else if (x == typeof(Guid))
            {
                str1 = "uniqueidentifier";
            }
            else if (x == typeof(double))
            {
                str1 = "float";
            }
            else if (x == typeof(float))
            {
                str1 = "float";
            }
            else if (x == typeof(bool))
            {
                str1 = "bit";
            }
            else if (x == typeof(byte[]))
            {
                str1 = "varbinary";
                str2 = "(MAX)";
            }
            if (!(str1 != ""))
            {
                throw new Exception("Type Not Currently Supported");
            }

            return IncludeBrackets ? "[" + str1 + "]" + str2 : str1 + " " + str2;
        }

        /// <summary>
        /// Converts a System.Data.DbType to a System.Data.SqlDbType
        /// </summary>
        /// <param name="DBType">In DBType</param>
        /// <returns>System.Data.SqlDbType</returns>
        public static SqlDbType ToSQLDataType(this DbType DBType)
        {
            switch (DBType)
            {
                case DbType.AnsiString:
                    return SqlDbType.Text;
                case DbType.Binary:
                    return SqlDbType.Image;
                case DbType.Byte:
                    return SqlDbType.SmallInt;
                case DbType.Boolean:
                    return SqlDbType.Bit;
                case DbType.Currency:
                    return SqlDbType.Money;
                case DbType.Date:
                    return SqlDbType.Date;
                case DbType.DateTime:
                    return SqlDbType.DateTime;
                case DbType.Decimal:
                    return SqlDbType.Decimal;
                case DbType.Double:
                    return SqlDbType.Float;
                case DbType.Guid:
                    return SqlDbType.UniqueIdentifier;
                case DbType.Int16:
                    return SqlDbType.SmallInt;
                case DbType.Int32:
                    return SqlDbType.Int;
                case DbType.Int64:
                    return SqlDbType.BigInt;
                case DbType.Object:
                    return SqlDbType.Structured;
                case DbType.SByte:
                    return SqlDbType.SmallInt;
                case DbType.Single:
                    return SqlDbType.Decimal;
                case DbType.String:
                    return SqlDbType.NVarChar;
                case DbType.Time:
                    return SqlDbType.Time;
                case DbType.UInt16:
                    return SqlDbType.Int;
                case DbType.UInt32:
                    return SqlDbType.BigInt;
                case DbType.UInt64:
                    return SqlDbType.BigInt;
                case DbType.VarNumeric:
                    return SqlDbType.BigInt;
                case DbType.AnsiStringFixedLength:
                    return SqlDbType.VarChar;
                case DbType.StringFixedLength:
                    return SqlDbType.NVarChar;
                case DbType.Xml:
                    return SqlDbType.Xml;
                case DbType.DateTime2:
                    return SqlDbType.DateTime2;
                case DbType.DateTimeOffset:
                    return SqlDbType.DateTimeOffset;
                default:
                    throw new Exception("Error Converting DataType: " + DBType.ToString());
            }
        }

        /// <summary>Converts DbType to String</summary>
        /// <param name="DBType">DBType</param>
        /// <returns>LowerCase String</returns>
        public static string ToDBStringCustom(this DbType DBType)
        {
            switch (DBType)
            {
                case DbType.AnsiString:
                    return "text";
                case DbType.Binary:
                    return "binary";
                case DbType.Byte:
                    return "tinyint";
                case DbType.Boolean:
                    return "bit";
                case DbType.Currency:
                    return "money";
                case DbType.Date:
                    return "datetime";
                case DbType.DateTime:
                    return "datetime";
                case DbType.Decimal:
                    return "decimal";
                case DbType.Double:
                    return "float";
                case DbType.Guid:
                    return "uniqueidentifier";
                case DbType.Int16:
                    return "smallint";
                case DbType.Int32:
                    return "int";
                case DbType.Int64:
                    return "bigint";
                case DbType.Object:
                    return "image";
                case DbType.String:
                    return "ntext";
                case DbType.AnsiStringFixedLength:
                    return "varchar";
                case DbType.StringFixedLength:
                    return "nvarchar";
                case DbType.DateTime2:
                    return "datetime";
                case DbType.DateTimeOffset:
                    return "datetime";
                default:
                    throw new Exception(DBType.ToString() + " Not supported");
            }
        }

        /// <summary>Converts DbType to String</summary>
        /// <param name="DBType">DBType</param>
        /// <returns>LowerCase String</returns>
        public static string ToCSharpString(this DbType DBType)
        {
            switch (DBType)
            {
                case DbType.AnsiString:
                    return "string";
                case DbType.Binary:
                    return "byte[]";
                case DbType.Byte:
                    return "int";
                case DbType.Boolean:
                    return "bool";
                case DbType.Currency:
                    return "double";
                case DbType.Date:
                    return "DateTime";
                case DbType.DateTime:
                    return "DateTime";
                case DbType.Decimal:
                    return "double";
                case DbType.Double:
                    return "double";
                case DbType.Guid:
                    return "Guid";
                case DbType.Int16:
                    return "int";
                case DbType.Int32:
                    return "int";
                case DbType.Int64:
                    return "long";
                case DbType.Object:
                    return "byte[]";
                case DbType.String:
                    return "string";
                case DbType.AnsiStringFixedLength:
                    return "string";
                case DbType.StringFixedLength:
                    return "string";
                case DbType.DateTime2:
                    return "DateTime";
                case DbType.DateTimeOffset:
                    return "DateTime";
                default:
                    throw new Exception(DBType.ToString() + " Not supported");
            }
        }

        /// <summary>Converts DbType to Nullable String</summary>
        /// <param name="DBType">DBType</param>
        /// <returns>LowerCase String</returns>
        public static string ToCSharpStringNullable(this DbType DBType)
        {
            switch (DBType)
            {
                case DbType.AnsiString:
                    return "string";
                case DbType.Binary:
                    return "byte[]";
                case DbType.Byte:
                    return "int?";
                case DbType.Boolean:
                    return "bool?";
                case DbType.Currency:
                    return "decimal?";
                case DbType.Date:
                    return "DateTime?";
                case DbType.DateTime:
                    return "DateTime?";
                case DbType.Decimal:
                    return "decimal?";
                case DbType.Double:
                    return "double?";
                case DbType.Guid:
                    return "Guid?";
                case DbType.Int16:
                    return "int?";
                case DbType.Int32:
                    return "int?";
                case DbType.Int64:
                    return "long?";
                case DbType.Object:
                    return "byte[]";
                case DbType.String:
                    return "string";
                case DbType.AnsiStringFixedLength:
                    return "string";
                case DbType.StringFixedLength:
                    return "string";
                case DbType.DateTime2:
                    return "DateTime?";
                case DbType.DateTimeOffset:
                    return "DateTime?";
                default:
                    throw new Exception(DBType.ToString() + " Not supported");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DBType"></param>
        /// <returns></returns>
        public static bool IsStringType(this DbType DBType) => DBType.ToCSharpString() == "string";

        public static DataTable ToDataTable(this SqlDataReader Reader)
        {
            DataTable dataTable;
            using (InternalDataAdapter internalDataAdapter = new InternalDataAdapter())
            {
                dataTable = internalDataAdapter.ConvertToDataTable(Reader);
            }

            return dataTable;
        }

    }
}
