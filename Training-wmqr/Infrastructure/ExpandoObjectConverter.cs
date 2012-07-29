using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Web.Script.Serialization;

namespace Training_wmqr.Infrastructure
{
  public class ExpandoObjectConverter : JavaScriptConverter
    {
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(ExpandoObject) })); }
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            ExpandoObject expando = (ExpandoObject)obj;

            if (expando != null)
            {
                // Create the representation.
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (KeyValuePair<string, object> item in expando)
                {
                    var key = item.Key;
                    var value = item.Value ?? "";

                    //make my javascript backbone happy
                    if (item.Key == "Id" || item.Key == "ID")
                    {
                        result.Add(item.Key, value);
                        if (!result.ContainsKey("id")) result.Add("id", value);
                        continue;
                    }

                    if (value.GetType() == typeof(DateTime))
                        result.Add(key, value);
                    else if (value.GetType() == typeof(Boolean) || value.GetType() == typeof(int))
                        result.Add(key, value);
                    else
                        result.Add(key, value.ToString());

                }
                return result;
            }
            return new Dictionary<string, object>();
        }
        public override dynamic Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            dynamic result = new ExpandoObject();

            if (dictionary != null)
            {
                // Create the representation.
                var dc = (IDictionary<string, object>)result;
                foreach (KeyValuePair<string, object> item in dictionary)
                {
                    //Convert it back to c sharpness
                    var key = item.Key;
                    var value = item.Value ?? "";
                    if (key.Contains("Date"))
                        value = DateTimeOrNull(item.Value);
                    if (item.Key.ToLower() == "id")
                    {
                        if (!dc.ContainsKey("Id"))
                            dc.Add("Id", value);
                        continue;
                    }
                    dc.Add(key, value);

 
                }
            }
            return result;
        }

        private static object DateTimeOrNull(object value)
        {
            DateTime date;
            if (value.ToString().StartsWith("/Date("))
                return ParseJsonDateTime(value.ToString());
            return DateTime.TryParse((value ?? "").ToString(), out date) ? date : (object)DBNull.Value; ;
        }

        private static object ParseJsonDateTime(string value)
        {
            long result;
            if (!long.TryParse(value.Replace("/Date(", "").Replace(")/", ""), out result))
                return DBNull.Value;
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(result).ToLocalTime();
        }
    }
}