using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace BuilderInterpreter.Helper
{
    class RetryHandler : DelegatingHandler
    {
        private int MaxRetries = 3;

        public RetryHandler() : base(new HttpClientHandler())
        {
        }

        public RetryHandler(int maxRetries) : base(new HttpClientHandler())
        {
            MaxRetries = maxRetries;
        }

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
                catch (Exception e)
                {
                    throw;
                }
            } while (!response.IsSuccessStatusCode && ++tries < MaxRetries);

            return response;
        }
    }
}
