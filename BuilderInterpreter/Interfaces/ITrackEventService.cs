using BuilderInterpreter.Models;
using System.Threading.Tasks;

namespace BuilderInterpreter
{
    public interface ITrackEventService
    {
        Task<bool> RegisterEventTrack(TrackEvent trackEvent);

        Task<string[]> GetAllEventTrackCategories();
    }
}