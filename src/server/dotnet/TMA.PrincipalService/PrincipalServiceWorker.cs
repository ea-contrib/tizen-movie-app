using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TMA.Contracts.Dto;
using TMA.Contracts.Messages;
using TMA.MessageBus;

namespace TMA.PrincipalService
{
    public class PrincipalServiceWorker: IHostedService, IDisposable
    {
        private readonly IMessageBus _messageBus;
        private readonly PrincipalBlo _principalBlo;

        public PrincipalServiceWorker(IMessageBus messageBus, PrincipalBlo principalBlo)
        {
            _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
            _principalBlo = principalBlo ?? throw new ArgumentNullException(nameof(principalBlo));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        { 
            await _messageBus.SubscribeAsync<IsPrincipalCredentialsValidCommand, ResponseMessage<bool>>(x => _principalBlo.IsPrincipalCredentialsValid(x.Email, x.Password));
            await _messageBus.SubscribeAsync<GetPrincipalClaimsCommand, ResponseMessage<ClaimsDto>>(x => _principalBlo.GetPrincipalClaims(x.Email, x.Id));
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
        }

        public void Dispose()
        {
        }
    }
}
