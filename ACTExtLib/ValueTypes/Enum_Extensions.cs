namespace ACT.Core.Extensions
{
    public static class Enum_Extensions
    {


        public static Enum FromString(this Enum x, string v) => (Enum)Enum.Parse(x.GetType(), v);


    }
}
