using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMA.Contracts.Dto;
using TMA.Contracts.Messages;

namespace TMA.PrincipalService
{
    public class PrincipalBlo
    {
        public async Task<ResponseMessage<bool>> IsPrincipalCredentialsValid(string email, string password)
        {
            return new ResponseMessage<bool>(true);
        }

        public async Task<ResponseMessage<ClaimsDto>> GetPrincipalClaims(string email, int id)
        {
            return new ResponseMessage<ClaimsDto>(new ClaimsDto()
            {
                Claims = new List<Tuple<string, string, string>>()
            });
        }
    }
}
