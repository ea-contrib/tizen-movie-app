using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TMA.Contracts.Dto;
using TMA.Contracts.Messages;
using TMA.MessageBus;
using TMA.PrincipalService.Logic;

namespace TMA.PrincipalService
{
    public class PrincipalServiceWorker: IHostedService, IDisposable
    {
        private readonly IMessageBus _messageBus;
        private readonly PrincipalBlo _principalBlo;
        private readonly GrantBlo _grantBlo;

        public PrincipalServiceWorker(IMessageBus messageBus, PrincipalBlo principalBlo, GrantBlo grantBlo)
        {
            _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
            _principalBlo = principalBlo ?? throw new ArgumentNullException(nameof(principalBlo));
            _grantBlo = grantBlo ?? throw new ArgumentNullException(nameof(grantBlo));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        { 
            await _messageBus.SubscribeAsync<IsPrincipalCredentialsValidCommand, ResponseMessage<bool>>(x => _principalBlo.IsPrincipalCredentialsValid(x.Email, x.Password));
            await _messageBus.SubscribeAsync<GetPrincipalClaimsCommand, ResponseMessage<ClaimsDto>>(x => _principalBlo.GetPrincipalClaims(x.Email, x.Id));


            await _messageBus.SubscribeAsync<SaveCommand<GrantDto>, ResponseMessage>(x => _grantBlo.Save(x.Data));
            await _messageBus.SubscribeAsync<GetGrantsListCommand, ListResponseMessage<GrantDto>>(x => _grantBlo.List(x));
            await _messageBus.SubscribeAsync<RemoveGrantsCommand, ResponseMessage>(x => _grantBlo.Remove(x));
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
        }

        public void Dispose()
        {
        }
    }
}
