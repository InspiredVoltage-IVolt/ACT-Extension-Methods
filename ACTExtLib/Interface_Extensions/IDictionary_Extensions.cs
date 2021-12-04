using System.Collections.Specialized;
using System.Data;

namespace ACT.Core.Extensions
{



    public static class I_Dictionary_Extensions
    {
        public static Dictionary<string, string> ToDictionary(this NameValueCollection x)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (string key in x.Keys)
            {
                try
                {
                    dictionary.Add(key, x[key]);
                }
                catch
                {
                }
            }
            return dictionary;
        }

        public static NameValueCollection ToNameValueCollection<TKey, TValue>(
          this IDictionary<TKey, TValue> dict)
        {
            NameValueCollection nameValueCollection = new NameValueCollection();
            foreach (KeyValuePair<TKey, TValue> keyValuePair in dict)
            {
                string str = null;
                if (keyValuePair.Value != null)
                {
                    str = keyValuePair.Value.ToString();
                }

                nameValueCollection.Add(keyValuePair.Key.ToString(), str);
            }
            return nameValueCollection;
        }

        public static string DumpStringValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) => "{" + string.Join(",", dictionary.Select<KeyValuePair<TKey, TValue>, string>(kv => kv.Key.ToString() + "=" + kv.Value.ToString()).ToArray<string>()) + "}";

        public static string DumpStringToQueryString<TKey, TValue>(
          this IDictionary<TKey, TValue> dictionary)
        {
            return string.Join("&", dictionary.Select<KeyValuePair<TKey, TValue>, string>(kv => kv.Key.ToString().URLEncode() + "=" + kv.Value.ToString().URLEncode()).ToArray<string>());
        }

        public static DataTable ToDataTable(this List<Dictionary<string, string>> list)
        {
            DataTable dataTable = new DataTable();
            if (list.Count == 0)
            {
                return dataTable;
            }

            IEnumerable<string> source = list.SelectMany<Dictionary<string, string>, string>(dict => dict.Keys).Distinct<string>();
            dataTable.Columns.AddRange(source.Select<string, DataColumn>(c => new DataColumn(c)).ToArray<DataColumn>());
            foreach (Dictionary<string, string> dictionary in list)
            {
                DataRow row = dataTable.NewRow();
                foreach (string key in dictionary.Keys)
                {
                    row[key] = dictionary[key];
                }

                dataTable.Rows.Add(row);
            }
            return dataTable;
        }
    }
}
