using BuilderInterpreter.Models;
using RestEase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuilderInterpreter.Interfaces
{
    public interface IBlipProvider
    {
        [Header("Authorization")]
        string AuthorizationKey { get; set; }

        [Header("Content-Type", "application/json")]
        [Post("commands")]
        Task<BlipCommand> SendCommandAsync([Body] BlipCommand command);
    }
}
