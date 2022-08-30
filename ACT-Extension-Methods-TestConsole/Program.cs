using ACT.Core.Extensions;
using OfficeOpenXml;

namespace ACT_Extension_Methods_TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
	        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage _P = new ExcelPackage (new FileInfo ("d:\\tmp\\A.xlsx"));


            Console.WriteLine("Testing Excel To CSV");

            _P.ConvertExcelToCsv ("d:\\tmp\\b.csv");

            Console.WriteLine("Converted To CSV File.");


        }
    }
}