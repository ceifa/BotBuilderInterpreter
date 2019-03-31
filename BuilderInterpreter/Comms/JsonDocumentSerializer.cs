using Lime.Protocol;
using Lime.Protocol.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace BuilderInterpreter.Comms
{
    internal class JsonDocumentSerializer : JsonConverter
    {
        private readonly DocumentSerializer _serializer;

        public JsonDocumentSerializer()
        {
            _serializer = new DocumentSerializer();
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            return new Models.BuilderModels.Message
            {
                Id = jObject["id"].ToString(),
                Type = jObject["type"].ToString(),
                Content = _serializer.Deserialize(jObject["content"].ToString(), MediaType.Parse(jObject["type"].ToString()))
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
        }
    }
}
