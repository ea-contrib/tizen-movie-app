using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TMA.Contracts.Dto;
using TMA.Contracts.Messages;
using TMA.PrincipalService.Entities;
using TMA.PrincipalService.Repositories;

namespace TMA.PrincipalService.Logic
{
    public class GrantBlo
    {
        private readonly GrantRepository _repository;
        private readonly IMapper _mapper;

        public GrantBlo(GrantRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseMessage> Save(GrantDto data)
        {
            var grants = await _repository.List(data.Key, null, null, null);


            if (grants.Any())
            {
                var entity = grants.First();

                _mapper.Map<GrantDto, GrantEntity>(data, entity);

                await _repository.Update(entity);
            }
            else
            {
                var entity = _mapper.Map<GrantEntity>(data);

                await _repository.Insert(entity);
            }

            return new ResponseMessage();
        }

        public async Task<ListResponseMessage<GrantDto>> List(GetGrantsListCommand command)
        {
            var list = await _repository.List(command.Key, command.SubjectId, command.Type, command.ClientId);

            return new ListResponseMessage<GrantDto>()
            {
                Value = list.Select(_mapper.Map<GrantDto>).ToList()
            };
        }

        public async Task<ResponseMessage> Remove(RemoveGrantsCommand command)
        {
            var list = await _repository.List(command.Key, command.SubjectId, command.Type, command.ClientId);

            await _repository.Remove(list);

            return new ResponseMessage();
        }
    }
}
