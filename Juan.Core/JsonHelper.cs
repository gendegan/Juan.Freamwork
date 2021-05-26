using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Juan.Core
{
    public static class JsonHelper
    {
        public static T GetJsonValue<T>(this string json, string key)
        {
            JContainer container = json.JsonJContainer();
            if (container == null)
            {
                return default(T);
            }
            if (container[key] == null)
            {
                return default(T);
            }
            return container[key].ToObject<T>();
        }

        public static string Json(this object data, JsonSerializerSettings settings = null)
        {
            return data.JsonSerialize(settings);
        }

        public static T Json<T>(this string input, JsonSerializerSettings settings = null)
        {
            return input.JsonDeserialize<T>(settings);
        }

        public static string Json(this object data, Formatting formatting, JsonSerializerSettings settings = null)
        {
            return data.JsonSerialize(settings);
        }

        public static T JsonAnonymousType<T>(this string input, T anonymousTypeObject)
        {
            return JsonConvert.DeserializeAnonymousType<T>(input, anonymousTypeObject);
        }

        public static T JsonAnonymousType<T>(this string input, T anonymousTypeObject, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeAnonymousType<T>(input, anonymousTypeObject, settings);
        }

        public static object JsonDeserialize(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            return JsonConvert.DeserializeObject(input);
        }

        public static T JsonDeserialize<T>(this string input, JsonSerializerSettings settings = null)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return default(T);
            }
            if (settings!=null)
            {
                return JsonConvert.DeserializeObject<T>(input, settings);
            }
            return JsonConvert.DeserializeObject<T>(input);
        }

        public static object JsonDeserialize(this string input, Type type)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }
            return JsonConvert.DeserializeObject(input, type);
        }

        public static XNode JsonDeserializeXml(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }
            return JsonConvert.DeserializeXNode(input);
        }

        public static string JsonFormatOutput(this string jsonValue)
        {
            JsonSerializer serializer = new JsonSerializer();
            TextReader reader = new StringReader(jsonValue);
            JsonTextReader reader2 = new JsonTextReader(reader);
            object obj2 = serializer.Deserialize(reader2);
            if (obj2 != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj2);
                return textWriter.ToString();
            }
            return jsonValue;
        }

        public static JContainer JsonJContainer(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            return (JsonConvert.DeserializeObject(input) as JContainer);
        }

        public static JObject JsonJObject(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            return (JObject)JsonConvert.DeserializeObject(input);
        }

        public static string JsonSerialize(this object data, JsonSerializerSettings settings = null)
        {
            if (data == null)
            {
                return "";
            }
            if (settings!=null)
            {
                return JsonConvert.SerializeObject(data, settings);
            }
            return JsonConvert.SerializeObject(data);
        }

        public static string JsonSerialize(this object data, Formatting formatting, JsonSerializerSettings settings = null)
        {
            if (data == null)
            {
                return "";
            }
            if (settings!=null)
            {
                return JsonConvert.SerializeObject(data, formatting, settings);
            }
            return JsonConvert.SerializeObject(data, formatting);
        }

        public static string JsonSerializeXml(this string data)
        {
            return JsonConvert.SerializeXNode(XElement.Parse(data));
        }

        public static string JsonSerializeXml(this XNode data)
        {
            if (data == null)
            {
                return "";
            }
            return JsonConvert.SerializeXNode(data);
        }

        public static string SetValue(string json, string key, object value)
        {
            object o = value ?? "";
            JObject data = json.JsonJContainer() as JObject;
            if (data == null)
            {
                data = new JObject(new JProperty(key, JToken.FromObject(o)));
            }
            else if (data[key] != null)
            {
                data.Remove(key);
                data.Add(new JProperty(key, JToken.FromObject(o)));
            }
            else
            {
                data.Add(new JProperty(key, JToken.FromObject(o)));
            }
            return data.JsonSerialize(null);
        }
    }




}
