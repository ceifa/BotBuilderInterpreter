using BuilderInterpreter.Models;
using Lime.Protocol;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    public interface IVariableService
    {
        Task<object> GetVariableValueAsync(string name, UserContext userContext);

        Task<string> ReplaceVariablesInStringAsync(string source, UserContext userContext);

        Task<Document> ReplaceVariablesInDocumentAsync(Document source, UserContext userContext);

        Task<T> ReplaceVariablesInObjectAsync<T>(T source, UserContext userContext);
    }
}
