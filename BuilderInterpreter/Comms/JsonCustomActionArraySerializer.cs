using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using BuilderInterpreter.Models.BuilderModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace BuilderInterpreter.Comms
{
    internal class JsonCustomActionArraySerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var customActions = new List<CustomAction>();

            foreach (var jObj in JArray.Load(reader))
            {
                var jObject = JObject.FromObject(jObj);

                var title = jObject["$title"].ToString();
                var type = Enum.Parse<CustomActionType>(jObject["type"].ToString());
                ICustomActionSettingsBase settings;

                switch (type)
                {
                    case CustomActionType.ProcessHttp:
                        settings = JsonConvert.DeserializeObject<ProcessHttp>(jObject["settings"].ToString());
                        break;
                    case CustomActionType.TrackEvent:
                        settings = JsonConvert.DeserializeObject<TrackEventAction>(jObject["settings"].ToString());
                        break;
                    case CustomActionType.MergeContact:
                        settings = JsonConvert.DeserializeObject<MergeContact>(jObject["settings"].ToString());
                        break;
                    case CustomActionType.Redirect:
                        settings = JsonConvert.DeserializeObject<Redirect>(jObject["settings"].ToString());
                        break;
                    case CustomActionType.ManageList:
                        settings = JsonConvert.DeserializeObject<ManageList>(jObject["settings"].ToString());
                        break;
                    case CustomActionType.ExecuteScript:
                        settings = JsonConvert.DeserializeObject<ExecuteScript>(jObject["settings"].ToString());
                        break;
                    default:
                        throw new NotImplementedException(nameof(settings));
                }

                customActions.Add(new CustomAction
                {
                    Title = title,
                    Type = type,
                    Settings = settings
                });
            }

            return customActions.ToArray();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

        }
    }
}
