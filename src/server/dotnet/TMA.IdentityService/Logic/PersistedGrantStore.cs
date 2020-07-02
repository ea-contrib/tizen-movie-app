using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using TMA.Contracts.Dto;
using TMA.Contracts.Messages;
using TMA.MessageBus;

namespace TMA.IdentityService.Logic
{
    public class PersistedGrantStore : IPersistedGrantStore
    {
        private readonly IMessageBus _messageBus;

        public PersistedGrantStore(IMessageBus messageBus)
        {
            _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
        }

        public async Task StoreAsync(PersistedGrant grant)
        {
            await _messageBus.PublishAsync<SaveCommand<GrantDto>, ResponseMessage>(new SaveCommand<GrantDto>(ToDto(grant)));
        }

        public GrantDto ToDto(PersistedGrant persistedGrant)
        {
            if (persistedGrant == null)
            {
                return null;
            }

            return new GrantDto()
            {
                ClientId = persistedGrant.ClientId,
                CreationTime = persistedGrant.CreationTime,
                Type = persistedGrant.Type,
                SubjectId = persistedGrant.SubjectId,
                Key = persistedGrant.Key,
                Data = persistedGrant.Data,
                Expiration = persistedGrant.Expiration,
            };
        }

        private PersistedGrant ToPersistedGrant(GrantDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            return new PersistedGrant()
            {
                ClientId = dto.ClientId,
                CreationTime = dto.CreationTime,
                Type = dto.Type,
                SubjectId = dto.SubjectId,
                Key = dto.Key,
                Data = dto.Data,
                Expiration = dto.Expiration
            };
        }

        public async Task<PersistedGrant> GetAsync(string key)
        {
            var grants = await _messageBus.PublishAsync<GetGrantsListCommand, ListResponseMessage<GrantDto>>(new GetGrantsListCommand()
            {
                Key = key
            });

            var grant = grants.Value.FirstOrDefault();

            return ToPersistedGrant(grant);
        }

        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            var grants = await _messageBus.PublishAsync<GetGrantsListCommand, ListResponseMessage<GrantDto>>(new GetGrantsListCommand()
            {
                SubjectId = subjectId
            });

            return grants.Value.Select(ToPersistedGrant).ToList();
        }

        public async Task RemoveAsync(string key)
        {
            await _messageBus.PublishAsync<RemoveGrantsCommand, ResponseMessage>(new RemoveGrantsCommand()
            {
                Key = key
            });
        }

        public async Task RemoveAllAsync(string subjectId, string clientId)
        {
            await _messageBus.PublishAsync<RemoveGrantsCommand, ResponseMessage>(new RemoveGrantsCommand()
            {
                SubjectId = subjectId,
                ClientId = clientId
            });
        }

        public async Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            await _messageBus.PublishAsync<RemoveGrantsCommand, ResponseMessage>(new RemoveGrantsCommand()
            {
                SubjectId = subjectId,
                ClientId = clientId,
                Type = type
            });
        }
    }
}