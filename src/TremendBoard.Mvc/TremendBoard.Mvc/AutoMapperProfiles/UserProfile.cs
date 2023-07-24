using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TremendBoard.Infrastructure.Services.DTO.User;
using TremendBoard.Mvc.Models.UserViewModels;

namespace TremendBoard.Mvc.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<UserDetailDTO, UserDetailViewModel>();
        }
    }
}
