using AutoMapper;
using TMA.Contracts.Dto;
using TMA.PrincipalService.Entities;

namespace TMA.PrincipalService.Mappings
{
    public class GrantProfile: Profile
    {
        public GrantProfile()
        {
            CreateMap<GrantEntity, GrantDto>()
                .ReverseMap();
        }
    }
}
