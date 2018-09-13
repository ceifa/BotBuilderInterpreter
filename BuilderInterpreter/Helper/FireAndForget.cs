using System;
using System.Threading.Tasks;

namespace BuilderInterpreter.Helper
{
    internal static class FireAndForget
    {
        public static void Run(Func<Task> task)
        {
            Task.Run(task).ConfigureAwait(false);
        }
    }
}
