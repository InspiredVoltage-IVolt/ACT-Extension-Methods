using OfficeOpenXml;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace ACT.Core.Extensions
{
    public static class MicrosoftExcel_Extensions
    {
        /// <summary>
        /// MAX 1000 Columns
        /// </summary>
        /// <param name="SelectedWorksheet"></param>
        /// <param name="Columns"></param>
        /// <returns></returns>
	    private static (int ColumnCount, int RowCount) GetMaxCellWithData(this ExcelWorksheet SelectedWorksheet, int BlankCountSkip = 4)
        {
            int _ColCount = 0;
            int _RowCount = 0;
            int _BlankCount = 0;

            for (var x = 1; x < 1000; x = x + 1)
            {
                var _tmpData = "";

                if (SelectedWorksheet.Cells[1, x].Formula.NullOrEmpty())
                {
                    if (SelectedWorksheet.Cells[1, x].Text.NullOrEmpty() == false)
                    {
                        _tmpData = SelectedWorksheet.Cells[1, x].Text.ToString(null);
                    }
                }
                else { _tmpData = SelectedWorksheet.Cells[1, x].Formula.ToString(null); }

                if (_tmpData.NullOrEmpty())
                {
	                if (_BlankCount > BlankCountSkip) { break; }

	                _BlankCount++;
                }
                else { _BlankCount = 0; _ColCount++; }
                
            }
            

            for (var y = 1; y < 100000; y = y + 1)
            {
	            for (var x = 1; x <= _ColCount; x = x + 1)
	            {
		            var _tmpData = "";

		            if (SelectedWorksheet.Cells[y, x].Formula.NullOrEmpty())
		            {
			            if (SelectedWorksheet.Cells[y, x].Text.NullOrEmpty() == false)
			            {
				            _tmpData = SelectedWorksheet.Cells[y, x].Text.ToString(null);
			            }
		            }
		            else { _tmpData = SelectedWorksheet.Cells[y, x].Formula.ToString(null); }

		            if (_tmpData.NullOrEmpty())
		            {
			            if (_BlankCount > BlankCountSkip) { break; }
			            _BlankCount++;
		            }
		            else { _BlankCount = 0; _RowCount++; }

		           
                }
            }

            return (_ColCount,_RowCount);
        }

        /// <summary>
        /// MAX 1000 Columns
        /// </summary>
        /// <param name="MyExcelPackage"></param>
        /// <param name="Output"></param>
        /// <param name="SheetNumber"></param>
        public static void ConvertExcelToCsv(this ExcelPackage MyExcelPackage, string Output, int SheetNumber = 0)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var ExcelExportFormat = new ExcelOutputTextFormat
            {
                TextQualifier = '"',
                Encoding = Encoding.UTF8
            };

            var _WorkSheet = MyExcelPackage.Workbook.Worksheets[SheetNumber];
            
           var RangeData = _WorkSheet.GetMaxCellWithData (10);

            var ColName = RangeData.ColumnCount.ToExcelColumnAddress();

            _WorkSheet.Cells[1, 1, RangeData.RowCount, RangeData.ColumnCount].SaveToText(new FileInfo(Output.EnsureEndsWith(".csv")), ExcelExportFormat);
        }
        public static string ToExcelColumnAddress(this int ColumnNumber)
        {
            if (ColumnNumber <= 26)
            {
                return Convert.ToChar(ColumnNumber + 64).ToString();
            }
            int div = ColumnNumber / 26;
            int mod = ColumnNumber % 26;
            if (mod == 0) { mod = 26; div--; }
            return ToExcelColumnAddress(div) + ToExcelColumnAddress(mod);
        }

        public static int ToEzcelColumnAdress(this string ExcelColumnAddress)
        {
            int[] digits = new int[ExcelColumnAddress.Length];
            for (int i = 0; i < ExcelColumnAddress.Length; ++i)
            {
                digits[i] = Convert.ToInt32(ExcelColumnAddress[i]) - 64;
            }
            int mul = 1; int res = 0;
            for (int pos = digits.Length - 1; pos >= 0; --pos)
            {
                res += digits[pos] * mul;
                mul *= 26;
            }
            return res;
        }
    }
}
