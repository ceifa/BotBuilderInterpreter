using BuilderInterpreter.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuilderInterpreter.Models
{
    public class UserContext
    {
        public string Identity { get; set; }

        public string StateId { get; set; }

        public Dictionary<string, object> Variables { get; set; }

        public UserContact Contact { get; set; }

        public Task<NlpResponse> NlpResponse => LazyNlpResponse.Value;

        private Lazy<Task<NlpResponse>> LazyNlpResponse { get; set; }

        internal void PopulateNlpResponse(string input, INlpProvider nlpProvider)
        {
            LazyNlpResponse = new Lazy<Task<NlpResponse>>(() => nlpProvider.GetNlpResponse(input, Identity));
        }
    }
}
