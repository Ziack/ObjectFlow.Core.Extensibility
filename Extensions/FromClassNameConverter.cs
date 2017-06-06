using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectFlow.Core.Extensibility
{
    public class FromClassNameConverter<T> : JsonConverter
    {
        private Type _detinationType;
        private object[] _parameters;

        public FromClassNameConverter(params object[] parameters)
        {
            _parameters = parameters;
            _detinationType = typeof(T);
        }

        public override bool CanConvert(Type objectType)
        {
            var canConvert = _detinationType == objectType || objectType.GetInterfaces().Any(x => x == _detinationType);

            return canConvert;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            if (reader.TokenType == JsonToken.StartObject)
            {
                var config = JObject.ReadFrom(reader);

                var type = config.Value<String>("type");
                var param = config.ToObject<IDictionary<String, String>>();

                return (T)Activator.CreateInstance(Type.GetType(type, throwOnError: true), new object[] { _parameters[0], param });
            }

            var typeName = Convert.ToString(reader.Value);

            return (T)Activator.CreateInstance(Type.GetType(typeName, throwOnError: true));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
