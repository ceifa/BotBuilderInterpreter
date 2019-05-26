using System;
using System.Threading.Tasks;
using BuilderInterpreter.Interfaces;
using BuilderInterpreter.Models;

namespace BuilderInterpreter
{
    public class BlipService : IBlipService
    {
        private readonly IBlipProvider _blipProvider;
        private readonly Configuration _configuration;

        public BlipService(IBlipProvider blipProvider, Configuration configuration)
        {
            _blipProvider = blipProvider;
            _configuration = configuration;
        }

        public Task<BlipCommand> SendCommandAsync(BlipCommand command)
        {
            if (string.IsNullOrEmpty(_configuration.AuthorizationKey))
                throw new ArgumentNullException("Authorization header is not present " +
                                                nameof(_configuration.AuthorizationKey));

            if (!_configuration.AuthorizationKey.StartsWith("Key "))
                throw new InvalidOperationException("Invalid Authorization Key " +
                                                    nameof(_configuration.AuthorizationKey));

            _blipProvider.AuthorizationKey = _configuration.AuthorizationKey;

            return _blipProvider.SendCommandAsync(command);
        }
    }
}