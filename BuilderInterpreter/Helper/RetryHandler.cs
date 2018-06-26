using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BuilderInterpreter.Helper
{
    class RetryHandler : DelegatingHandler
    {
        private const int MaxRetries = 3;

        public RetryHandler() : base(new HttpClientHandler())
        {
        }

        public RetryHandler(HttpMessageHandler innerHandler) : base(innerHandler) { }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage();
            var tries = 0;

            do
            {
                try
                {
                    response = await base.SendAsync(request, cancellationToken);
                }
                catch (Exception)
                {
                    throw;
                }
            } while (!response.IsSuccessStatusCode && ++tries < MaxRetries);

            return response;
        }
    }
}
