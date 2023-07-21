using AutoMapper;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Services.DTO.ProjectDTO;
using TremendBoard.Mvc.Models.ProjectViewModels;

namespace TremendBoard.Mvc.AutoMapperProfiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile() 
        {
            CreateMap<ProjectResponse, ProjectDetailViewModel>();
            CreateMap<ProjectDetailViewModel, ProjectUpdateRequest>();
            CreateMap<ProjectUpdateRequest, ProjectResponse>();
            CreateMap<ProjectUserDetailDTO, ProjectUserDetailViewModel>();
        }
    }
}
