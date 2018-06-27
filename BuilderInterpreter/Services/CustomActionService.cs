using BuilderInterpreter.Models;
using BuilderInterpreter.Models.BuilderModels;
using System.Threading.Tasks;

namespace BuilderInterpreter.Services
{
    class CustomActionService
    {
        public async Task ExecuteCustomActions(CustomAction[] customActions)
        {
            foreach(var customAction in customActions)
            {
                switch (customAction.Settings)
                {
                    case ProcessHttp settings:
                        break;
                    case TrackEventAction settings:
                        break;
                    case MergeContact settings:
                        break;
                    case Redirect settings:
                        break;
                    case ManageList settings:
                        break;
                    case ExecuteScript settings:
                        break;
                }
            }
        }
    }
}
