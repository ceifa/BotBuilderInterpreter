using BuilderInterpreter.Helper;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using RestEase;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BuilderInterpreter.Services
{
    public class BlipService
    {
        private readonly IBlipProvider _blipProvider;
        public static string AuthorizationKey { private get; set; }

        public BlipService(IBlipProvider blipProvider)
        {
            _blipProvider = blipProvider;
        }

        public Task<BlipCommand> SendCommandAsync(BlipCommand command)
        {
            if (string.IsNullOrEmpty(AuthorizationKey))
                throw new ArgumentNullException("Authorization header is not present " + nameof(AuthorizationKey));
            _blipProvider.AuthorizationKey = AuthorizationKey;
            return _blipProvider.SendCommandAsync(command);
        }
    }
}
