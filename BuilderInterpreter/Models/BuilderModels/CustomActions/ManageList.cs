using BuilderInterpreter.Interfaces;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models.BuilderModels
{
    internal class ManageList
    {
        [JsonProperty("listName")]
        public string ListName { get; set; }

        [JsonProperty("action")]
        public ManageListAction Action { get; set; }
    }
}