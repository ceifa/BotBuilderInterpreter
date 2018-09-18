using BuilderInterpreter.Interfaces;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models.BuilderModels
{
    internal class ManageList : ICustomActionPayload
    {
        [JsonProperty("listName")]
        public string ListName { get; set; }

        [JsonProperty("action")]
        public ManageListAction Action { get; set; }
    }
}