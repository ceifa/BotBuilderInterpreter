using BuilderInterpreter.Enums;
using Newtonsoft.Json;

namespace BuilderInterpreter.Models.BuilderModels
{
    class ManageList : CustomActionSettingsBase
    {
        [JsonProperty("listName")]
        public string ListName;
        [JsonProperty("action")]
        public ManageListAction Action;
    }
}
