using System.Threading.Tasks;
using BuilderInterpreter.Models;

namespace BuilderInterpreter
{
    internal interface ITrackEventService
    {
        Task<bool> RegisterEventTrack(TrackEvent trackEvent);

        Task<string[]> GetAllEventTrackCategories();
    }
}