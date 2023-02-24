using ACT.Core.Constants;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

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
         bool _TmpReturn = false;

         if (bool.TryParse(x.ToString(), out _TmpReturn)) { return (bool?)_TmpReturn; }
         return (bool?)null;
      }

      public static Guid? ToGuid(this object x)
      {
         return x.ToString().TryToGuid();
      }

      public static int? ToNullableInt(this object x)
      {
         int? _TmpReturn = null;
         _TmpReturn = (int?)x.ToString().ToInt(Common_Constants.TESTINTVALUE);

         if (_TmpReturn.Value == Common_Constants.TESTINTVALUE) { return (int?)null; }

         return _TmpReturn;
      }

      public static int ToInt(this object x, int _Default = 0) { return x.ToString().ToInt(_Default); }

      public static double ToDouble(this object x, double _Default = 0.0d)
      {
         return x.ToString().ToDouble(_Default);
      }

      public static long ToLong(this object x, long _Default = 0)
      {
         return x.ToString().ToLong(_Default);
      }

      public static ulong ToULong(this object x, ulong _Default = 0)
      {
         return x.ToString().ToULong(_Default);
      }

      public static float ToFloat(this object x, float _Default = 0.0f)
      {
         return x.ToString().ToFloat(_Default);
      }

      public static bool ToBool(this object x, bool _Default = false)
      {
         return x.ToString().ToBool(_Default);
      }

      public static short ToShort(this object x, short _Default = 0)
      {
         return x.ToString().ToShort(_Default);
      }

      public static ushort ToUShort(this object x, ushort _Default = 0)
      {
         return x.ToString().ToUShort(_Default);
      }

      public static decimal ToDecimal(this object x, Decimal _Default = 0M)
      {
         return x.ToString().ToDecimal(_Default);
      }

      public static DateTime? ToDateTime(this object x)
      {
         DateTime result;
         if (DateTime.TryParse(x.ToString(), out result)) { return (DateTime?)result; }
         return null;
      }

      public static string TryToString(this object x)
      {
         if (x == null) { return ""; }
         return x.ToString();
      }

      public static string TryToString(this object x, string DefaultVal = "")
      {
         if (x == null) { return DefaultVal; }
         return x.ToString();
      }

      /// <summary>
      /// Uses JConvert Because C# protects us little kids.
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public static byte[] ObjectToByteArray(this object obj)
      {
         if (obj == null) {return null; }

         var _obj = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
         if (_obj == null) { return null; }

         return _obj.ToBytes();


         /*
          //Figure out how big TestStruct is
          var size = Marshal.SizeOf(obj);
          //Make an array large enough to hold all the bytes required
          var array = new byte[size];
          //Get a pointer to the struct
          var itemPtr = &obj;
          //Change the type of the pointer from TestStruct* to byte*
          var itemBytes = (byte*)itemPtr;
          //Iterate from the first byte in the data to the last, copying the values into our
          for (var i = 0; i < size; ++i) { array[i] = itemBytes[i]; }
          //Return the bytes that were found in the instance of TestStruct
          return array;
         */
      }

      /// <summary>
      /// Dynamic Convert Type - Simple Types Only
      /// </summary>
      /// <param name="obj"></param>
      /// <param name="t"></param>
      /// <returns></returns>
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
