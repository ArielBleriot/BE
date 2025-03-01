using AutoMapper;
using BridgeRTU.Domain;
using BridgeRTU.Services.Activities.Dto;

namespace BridgeRTU.Mappings
{
    public class ActivityProfile : Profile
    {
        public ActivityProfile()
        {
            CreateMap<CreateActivityDto, Activity>();
            CreateMap<UpdateActivityDto, Activity>();
            CreateMap<Activity, ActivityDto>()
               .ForMember(x => x.Interests, cd => cd.MapFrom(map => map.Interest));
        }
    }

}
