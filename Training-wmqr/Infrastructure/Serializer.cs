using System.Collections.Generic;
using System.Dynamic;
using System.Web.Script.Serialization;

namespace Training_wmqr.Infrastructure
{
    public static class Serializer
    {
        public static string ToJSON(dynamic content)
        {
            if (content == null) return "";
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new ExpandoObjectConverter() });
            return serializer.Serialize(content);
        }

        public static ExpandoObject ToExpando(string content)
        {
            return ToExpando<ExpandoObject>(content) ?? new ExpandoObject();
        }

        public static IList<ExpandoObject> ToExpandoList(string content)
        {
            return ToExpando<IList<ExpandoObject>>(content) ?? new List<ExpandoObject>();
        }

        private static T ToExpando<T>(string content) where T : class
        {
            if (content == null) return null;
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new ExpandoObjectConverter() });
            return serializer.Deserialize<T>(content);
        }
    }
}