using System.Threading.Tasks;
using Lime.Protocol;

namespace BuilderInterpreter
{
    internal interface IDistributionListService
    {
        Task<bool> AddMemberOrCreateList(string listIdentity, string userIdentity);

        Task<bool> AddMemberToList(string listIdentity, string userIdentity);

        Task<bool> RemoveMemberFromList(string listIdentity, string userIdentity);

        Task<bool> SendMessageToList(string listIdentity, Document message);

        Task<bool> CreateListAsync(string listIdentity);

        Task<string[]> GetAllListsAsync();
    }
}