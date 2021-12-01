namespace ACT.Core.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// An IEnumerable&lt;T&gt; extension method that first or default from many.
        /// </summary>
        /// <remarks>   Mark Alicz, 10/28/2016. </remarks>
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="source">           The source to act on. </param>
        /// <param name="childrenSelector"> The children selector. </param>
        /// <param name="condition">        The condition. </param>
        /// <returns>   A T. </returns>
        public static T FirstOrDefaultFromMany<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> childrenSelector, Predicate<T> condition)
        {
            if (source == null || !source.Any<T>())
            {
                return default(T);
            }

            T obj = source.FirstOrDefault(t => condition(t));

            if (obj == null) { return default(T); }

            return !object.Equals(obj, default(T)) ? obj : source.SelectMany<T, T>(childrenSelector).FirstOrDefaultFromMany<T>(childrenSelector, condition);
        }

        public static bool? ToBool(this object x)
        {
            try
            {
                return new bool?(Convert.ToBoolean(x));
            }
            catch
            {
                return new bool?();
            }
        }

        public static Guid? ToGuid(this object x)
        {
            try
            {
                return x.ToString().TryToGuid();
            }
            catch
            {
                return new Guid?();
            }
        }

        public static int? ToNullableInt(this object x)
        {
            if (x == null || x == DBNull.Value)
            {
                return new int?();
            }

            int num = ToInt(x.ToString(), -1);
            return num == -1 ? new int?() : new int?(num);
        }

        public static int ToInt(this object x, int _Default = 0) => x == null || x == DBNull.Value ? _Default : ToInt(x.ToString(), _Default);

        public static double ToDouble(this object x, double _Default = 0.0)
        {
            if (x.TryToString() == null)
            {
                return _Default;
            }

            try
            {
                return Convert.ToDouble(x);
            }
            catch
            {
                return _Default;
            }
        }

        public static long ToLong(this object x, long _Default = 0) => x == null || x == DBNull.Value ? _Default : ToLong(x.ToString(), _Default);

        public static ulong ToULong(this object x, ulong _Default = 0)
        {
            if (x == null || x == DBNull.Value)
            {
                return _Default;
            }

            try
            {
                return Convert.ToUInt64(x);
            }
            catch
            {
                return _Default;
            }
        }

        public static float ToFloat(this object x, float _Default = 0.0f)
        {
            if (x.TryToString() == null)
            {
                return _Default;
            }

            try
            {
                return Convert.ToSingle(x);
            }
            catch
            {
                return _Default;
            }
        }

        public static bool ToBool(this object x, bool _Default = false) => x == null || x == DBNull.Value ? _Default : ToBool(x.ToString(), _Default);

        public static short ToShort(this object x, short _Default = 0)
        {
            if (x == null || x == DBNull.Value)
            {
                return _Default;
            }

            try
            {
                return Convert.ToInt16(x);
            }
            catch
            {
                return _Default;
            }
        }

        public static ushort ToUShort(this object x, ushort _Default = 0)
        {
            if (x == null || x == DBNull.Value)
            {
                return _Default;
            }

            try
            {
                return Convert.ToUInt16(x);
            }
            catch
            {
                return _Default;
            }
        }

        public static Decimal ToDecimal(this object x, Decimal _Default = 0M) => x == null || x == DBNull.Value ? _Default : ToDecimal(x.ToString(), _Default);

        public static DateTime? ToDateTime(this object x)
        {
            try
            {
                return new DateTime?(Convert.ToDateTime(Convert.ToDateTime(x).ToString("yyyy-MM-dd HH:mm:ss")));
            }
            catch
            {
                return new DateTime?();
            }
        }

        public static string TryToString(this object x)
        {
            try
            {
                return x.ToString();
            }
            catch
            {
                return null;
            }
        }

        public static string TryToString(this object x, string DefaultVal = "")
        {
            try
            {
                return x.ToString();
            }
            catch
            {
                return DefaultVal;
            }
        }

        public static byte[] ObjectToByteArray(this object obj)
        {
            // TODO
            return null;
            //int arrayLength = 1024;
            //// Create random data to write to the stream.
            //byte[] dataArray = new byte[arrayLength];
            //new Random().NextBytes(dataArray);

            //BinaryWriter bw = obj as BinaryWriter;
            //BinaryReader br = new BinaryReader(bw.BaseStream);

            //// Set Position to the beginning of the stream.
            //br.BaseStream.Position = 0;

            //// Read and verify the data.
            //byte[] verifyArray = br.ReadBytes(arrayLength);
            //if (verifyArray.Length != arrayLength) { throw new Exception("Error Verifying - Error Writing"); }

            //for (int i = 0; i < arrayLength; i++)
            //{
            //    if (verifyArray[i] != dataArray[i])
            //    {
            //        Console.WriteLine("Error writing the data.");
            //        return null;
            //    }
            //}

            //Console.WriteLine("The data was written and verified.");
            //return dataArray;
        }

        public static object CorrectTypeValue(this object obj, Type t)
        {
            if (t == typeof(string))
            {
                return obj == DBNull.Value ? "" : obj.TryToString("");
            }

            if (t == typeof(int) || t == typeof(int) || t == typeof(int?))
            {
                return obj.ToInt();
            }

            if (t == typeof(double))
            {
                return obj.ToDouble();
            }

            if (t == typeof(float))
            {
                return obj.ToFloat();
            }

            if (t == typeof(Decimal))
            {
                return obj.ToDecimal();
            }

            if (t == typeof(long) || t == typeof(long))
            {
                return obj.ToLong();
            }

            if (t == typeof(ulong))
            {
                return obj.ToULong();
            }

            return t == typeof(short) || t == typeof(short) ? obj.ToShort() : obj;
        }
    }
}
