using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using TremendBoard.Infrastructure.Services.DTO.RoleDTO;
using TremendBoard.Mvc.Models.RoleViewModels;

namespace TremendBoard.Mvc.AutoMapperProfiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<ApplicationRoleDetailDTO, ApplicationRoleDetailViewModel>();
        }
    }
}
