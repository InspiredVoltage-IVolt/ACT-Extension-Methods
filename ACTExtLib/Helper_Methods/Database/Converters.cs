using Newtonsoft.Json;
using System.Data;

namespace ACT.Core.Helper.Database.Converters
{
    /// <summary>
    /// Converts a <see cref="T:System.Data.DataRow" /> object to and from JSON.
    /// </summary>
    public class DataRowConverter : JsonConverter
    {
        /// <summary>Writes the JSON representation of the object.</summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        public override void WriteJson(JsonWriter writer, object dataRow, JsonSerializer SER)
        {
            DataRow dataRow1 = dataRow as DataRow;
            JsonSerializer jsonSerializer = new JsonSerializer();
            writer.WriteStartObject();
            foreach (DataColumn column in dataRow1.Table.Columns)
            {
                writer.WritePropertyName(column.ColumnName);
                jsonSerializer.Serialize(writer, dataRow1[column]);
            }
            writer.WriteEndObject();
        }

        /// <summary>
        /// Determines whether this instance can convert the specified value type.
        /// </summary>
        /// <param name="valueType">Type of the value.</param>
        /// <returns>
        ///     <c>true</c> if this instance can convert the specified value type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type valueType) => typeof(DataRow).IsAssignableFrom(valueType);

        /// <summary>Reads the JSON representation of the object.</summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(
          JsonReader reader,
          Type objectType,
          object obj,
          JsonSerializer SER = null)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Data Table Converter
    /// </summary>
    public class DataTableConverter : JsonConverter
    {
        /// <summary>Writes the JSON representation of the object.</summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        public override void WriteJson(JsonWriter writer, object dataTable, JsonSerializer SER = null)
        {
            DataTable dataTable1 = dataTable as DataTable;
            DataRowConverter dataRowConverter = new DataRowConverter();
            writer.WriteStartObject();
            writer.WritePropertyName("Rows");
            writer.WriteStartArray();
            foreach (DataRow row in (InternalDataCollectionBase)dataTable1.Rows)
            {
                dataRowConverter.WriteJson(writer, row, SER);
            }

            writer.WriteEndArray();
            writer.WriteEndObject();
        }

        /// <summary>
        /// Determines whether this instance can convert the specified value type.
        /// </summary>
        /// <param name="valueType">Type of the value.</param>
        /// <returns>
        ///     <c>true</c> if this instance can convert the specified value type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type valueType) => typeof(DataTable).IsAssignableFrom(valueType);

        /// <summary>Reads the JSON representation of the object.</summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(
          JsonReader reader,
          Type objectType,
          object obj,
          JsonSerializer SER = null)
        {
            throw new NotImplementedException();
        }
    }


    public class DataSetConverter : JsonConverter
    {
        /// <summary>Writes the JSON representation of the object.</summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        public override void WriteJson(JsonWriter writer, object dataset, JsonSerializer SER = null)
        {
            DataSet dataSet = dataset as DataSet;
            DataTableConverter dataTableConverter = new DataTableConverter();
            writer.WriteStartObject();
            writer.WritePropertyName("Tables");
            writer.WriteStartArray();
            foreach (DataTable table in dataSet.Tables)
            {
                dataTableConverter.WriteJson(writer, table, SER);
            }

            writer.WriteEndArray();
            writer.WriteEndObject();
        }

        /// <summary>
        /// Determines whether this instance can convert the specified value type.
        /// </summary>
        /// <param name="valueType">Type of the value.</param>
        /// <returns>
        ///     <c>true</c> if this instance can convert the specified value type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type valueType) => typeof(DataSet).IsAssignableFrom(valueType);

        /// <summary>Reads the JSON representation of the object.</summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(
          JsonReader reader,
          Type objectType,
          object obj,
          JsonSerializer SER = null)
        {
            throw new NotImplementedException();
        }
    }
}
