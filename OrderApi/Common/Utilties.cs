namespace Common
{
    public static class Utilties
    {
        public delegate bool TryParseHandler<T>(string s, out T parsedObject);

        public static T? SafeParse<T>(object obj, TryParseHandler<T> tryParseHandler) where T: struct
        {
            if (obj == null) return null;

            if(tryParseHandler(obj.SafeToString(), out T parsedObject))
                return parsedObject;

            return null;
        }

        public static string SafeToString(this object obj)
        {
            if (obj == null) return null;
            return obj.ToString();
        }

         
    }
}
