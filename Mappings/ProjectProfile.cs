using AutoMapper;
using BridgeRTU.Domain;
using BridgeRTU.Services.Activities.Dto;
using BridgeRTU.Services.Comments.Dto;
using BridgeRTU.Services.Projects.Dto;

namespace BridgeRTU.Mappings
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectDetailsDto, Project>();
            CreateMap<Project, ProjectDetailsDto>();
            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(opt => opt.Student.FullName));
        }
    }
}
