using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TMA.Contracts.Dto;
using TMA.Contracts.Messages;
using TMA.Identity;
using TMA.PrincipalService.Repositories;

namespace TMA.PrincipalService.Logic
{
    public class PrincipalBlo
    {
        private readonly PrincipalRepository _repository;
        private readonly PasswordHasher _hasher;

        public PrincipalBlo(PrincipalRepository repository, PasswordHasher hasher)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
        }

        public async Task<ResponseMessage<bool>> IsPrincipalCredentialsValid(string email, string password)
        {
            var principal = await _repository.GetPrincipalByEmail(email);

            if (principal == null || string.IsNullOrEmpty(password))
            {
                return new ResponseMessage<bool>(false);
            }

            if (_hasher.Verify(password, principal.PasswordHash, principal.PasswordSalt))
            {
                return new ResponseMessage<bool>(true);
            }

            return new ResponseMessage<bool>(false);
        }

        public async Task<ResponseMessage<ClaimsDto>> GetPrincipalClaims(string email, int id)
        {
            var principal = await _repository.GetPrincipalByEmail(email) ?? await _repository.GetById(id);

            if (principal == null)
            {
                var response = new ResponseMessage<ClaimsDto>();
                return response;
            }

            var identity = new ClaimsIdentity()
                .SetEmail(principal.Email)
                .SetSubjectId(principal.Id.ToString());


            return new ResponseMessage<ClaimsDto>(new ClaimsDto()
            {
                Claims = identity.Claims.Select(x => new Tuple<string, string, string>(x.Type, x.Value, x.ValueType)).ToList()
            });
        }
    }
}
