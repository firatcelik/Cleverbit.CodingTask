using AutoMapper;
using Cleverbit.CodingTask.Data.Dto;
using Cleverbit.CodingTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cleverbit.CodingTask.Data.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Match, MatchDto>().ReverseMap();

            CreateMap<MatchParticipant, MatchParticipantDto>()
                .ForMember(x => x.UserName, d => d.MapFrom(z => z.Participant.UserName))
                .ForMember(x => x.MatchName, d => d.MapFrom(z => z.Match.MatchName))
                .ReverseMap();

            CreateMap<User,UserDto>().
               ForMember(x => x.Password, opt => opt.Ignore())
               .ReverseMap();
        }
    }
}
