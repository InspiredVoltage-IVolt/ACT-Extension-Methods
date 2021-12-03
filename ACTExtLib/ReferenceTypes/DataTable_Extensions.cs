using ACT.Core.Helper.Database.Converters;
using FastExcel;
using Newtonsoft.Json;
using System.Data;

namespace ACT.Core.Extensions
{
    public static class DataTableExtensions
    {
        public static string ToJSON(this DataTable value)
        {
            Type type = value.GetType();
            JsonSerializer jsonSerializer = new JsonSerializer();
            jsonSerializer.NullValueHandling = NullValueHandling.Ignore;
            jsonSerializer.ObjectCreationHandling = ObjectCreationHandling.Replace;
            jsonSerializer.MissingMemberHandling = MissingMemberHandling.Ignore;
            jsonSerializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            if (type == typeof(DataRow))
            {
                jsonSerializer.Converters.Add(new DataRowConverter());
            }
            else if (type == typeof(DataTable))
            {
                jsonSerializer.Converters.Add(new DataTableConverter());
            }
            else if (type == typeof(DataSet))
            {
                jsonSerializer.Converters.Add(new DataSetConverter());
            }

            StringWriter stringWriter = new StringWriter();
            JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter);
            jsonTextWriter.Formatting = Formatting.Indented;
            jsonTextWriter.QuoteChar = '"';
            jsonSerializer.Serialize(jsonTextWriter, value);
            string str = stringWriter.ToString();
            jsonTextWriter.Close();
            stringWriter.Close();
            return str;
        }

        public static DataTable FromDataRowArray(this DataTable x, DataRow[] Data) => ((IEnumerable<DataRow>)Data).CopyToDataTable<DataRow>();

        /// <summary>Datatable To Excel File (xlsx ONLY)</summary>
        /// <param name="x"></param>
        /// <param name="CreateHeaderRows"></param>
        public static void ToExcelFile(
          this DataTable x,
          string ExcelFileName,
          bool CreateHeaderRows)
        {
            string fileName = ExcelFileName;
            string fromFileLocation = ExcelFileName.GetDirectoryFromFileLocation();
            if (!fromFileLocation.DirectoryExists())
            {
                fromFileLocation.CreateDirectoryStructure();
            }

            Worksheet worksheet = new Worksheet();
            List<Row> rowList = new List<Row>();
            List<Cell> cellList = new List<Cell>();
            int columnNumber1 = 1;
            int rowNumber1 = 1;
            foreach (DataColumn column in x.Columns)
            {
                cellList.Add(new Cell(columnNumber1, column.ColumnName));
                ++columnNumber1;
            }
            rowList.Add(new Row(rowNumber1, cellList));
            int rowNumber2 = rowNumber1 + 1;
            int columnNumber2 = 1;
            foreach (DataRow row in (InternalDataCollectionBase)x.Rows)
            {
                cellList.Clear();
                foreach (DataColumn column in x.Columns)
                {
                    cellList.Add(new Cell(columnNumber2, row[column.ColumnName]));
                    ++columnNumber2;
                }
                rowList.Add(new Row(rowNumber2, cellList));
                ++rowNumber2;
            }
            worksheet.Rows = rowList;
            using (FastExcel.FastExcel fastExcel = new FastExcel.FastExcel(new FileInfo(fileName), new FileInfo(ExcelFileName)))
            {
                fastExcel.Write(worksheet, 1);
            }
        }
    }


}
