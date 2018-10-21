using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BuilderInterpreter.Helper
{
    internal class RetryHandler : DelegatingHandler
    {
        private readonly int MaxRetries = 3;

        public RetryHandler() : base(new HttpClientHandler())
        {
        }

        public RetryHandler(int maxRetries) : base(new HttpClientHandler())
        {
            MaxRetries = maxRetries;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = default;
            var tries = 0;

            do
            {
                try
                {
                    response = await base.SendAsync(request, cancellationToken);
                }
                catch
                {
                    if (tries - 1 == MaxRetries)
                        throw;
                }
            } while (response?.IsSuccessStatusCode != true && ++tries < MaxRetries);

            return response;
        }
    }
}
