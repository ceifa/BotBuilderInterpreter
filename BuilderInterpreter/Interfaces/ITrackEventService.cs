using BuilderInterpreter.Models;
using System.Threading.Tasks;

namespace BuilderInterpreter
{
    internal interface ITrackEventService
    {
        Task<bool> RegisterEventTrack(TrackEvent trackEvent);

        Task<string[]> GetAllEventTrackCategories();
    }
}