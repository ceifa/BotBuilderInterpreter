﻿using System;
using System.Net.Http;
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
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage();
            var tries = 0;

            do
            {
                try
                {
                    response = base.SendAsync(request, cancellationToken).Result;
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
