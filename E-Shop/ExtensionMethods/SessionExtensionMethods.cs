using Newtonsoft.Json;

namespace E_Shop.ExtensionMethods
{
    public static class SessionExtensionMethods
    {

        //Session’larımızda array list, object bir nesne döndermek için;
        public static void SetObject(this ISession session, string key, object value)
        {
            string objectString = JsonConvert.SerializeObject(value);//value serilize ediliyor.
            session.SetString(key, objectString);
        }
        public static T GetObject<T>(this ISession session, string key) where T : class
        {
            string objectString = session.GetString(key);//key in string karşılığını ver
            //böyle bir değer yoksa:
            if (string.IsNullOrEmpty(objectString))
            {
                return null;
            }
            //varsa:
            T value = JsonConvert.DeserializeObject<T>(objectString);//stringi bana t türünde ver yani object olarak-deserilize işlemi-
            return value;

        }
    }
}
