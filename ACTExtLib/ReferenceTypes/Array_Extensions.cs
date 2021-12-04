namespace ACT.Core.Extensions.ReferenceTypes
{
    public static class Array_Extensions
    {
        public static T FirstOrDefault<T>(this Array x)
        {
            return x.Length == 0 ? default(T) : (T)x.GetEnumerator().Current;
        }
    }
}
