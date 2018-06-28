using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;
using System;
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
            if (AuthorizationKey.IndexOf("Key ") != 0)
                throw new InvalidOperationException("Invalid Authorization Key " + nameof(AuthorizationKey));

            _blipProvider.AuthorizationKey = AuthorizationKey;
            return _blipProvider.SendCommandAsync(command);
        }
    }
}
