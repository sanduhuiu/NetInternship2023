using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Services.DTO.ProjectDTO;
using TremendBoard.Infrastructure.Services.DTO.RoleDTO;
using TremendBoard.Infrastructure.Services.DTO.User;
using TremendBoard.Infrastructure.Services.Enums;
using TremendBoard.Infrastructure.Services.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddAsync(ProjectAddRequest model)
        {   
            await _unitOfWork.Project.AddAsync(new Project
            {
                Name = model.Name,
                Description = model.Description,
                ProjectStatus = model.ProjectStatus,
                Deadline = model.Deadline,
                CreatedDate = DateTime.Now
            });

            await _unitOfWork.SaveAsync();
        }
        public async Task<ProjectResponse> GetByIdAsync(string id)
        {
            /*Project projectFromDb = await _unitOfWork.Project.GetByIdAsync(id);

            return new ProjectResponse
            {
                Id = projectFromDb.Id,
                Name = projectFromDb.Name,
                Description = projectFromDb.Description,
                ProjectStatus = projectFromDb.ProjectStatus,
                Deadline = projectFromDb.Deadline,
            };*/

            var project = await _unitOfWork.Project.GetByIdAsync(id);

            if (project == null)
            {
                throw new ApplicationException($"Unable to load project with ID '{id}'.");
            }

            var users = await _unitOfWork.User.GetAllAsync();
            var usersView = users.Select(user => new UserDetailDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });

            var roles = await _unitOfWork.Role.GetAllAsync();
            var rolesView = roles
                .Where(x => x.Name != Role.Admin.ToString())
                .OrderBy(x => x.Name)
                .Select(r => new ApplicationRoleDetailDTO
                {
                    Id = r.Id,
                    RoleName = r.Name,
                    Description = r.Description
                });

            var model = new ProjectResponse
            {
                Id = id,
                Name = project.Name,
                Description = project.Description,
                ProjectStatus = project.ProjectStatus,
                Deadline = project.Deadline,
                ProjectUsers = new List<ProjectUserDetailDTO>(),
                Users = usersView,
                Roles = rolesView
            };

            var userRoles = _unitOfWork.Project.GetProjectUserRoles(id);

            foreach (var userRole in userRoles)
            {
                var user = users.FirstOrDefault(x => x.Id == userRole.UserId);
                var role = roles.FirstOrDefault(x => x.Id == userRole.RoleId);

                var projectUser = new ProjectUserDetailDTO
                {
                    ProjectId = id,
                    UserId = userRole.UserId,
                    RoleId = userRole.RoleId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserRoleName = role.Name
                };

                model.ProjectUsers.Add(projectUser);
            }

            return model;
        }

        public async Task<ProjectResponse> UpdateAsync(ProjectUpdateRequest model)
        {
            var projectId = model.Id;
            var project = await _unitOfWork.Project.GetByIdAsync(projectId);

            project.Name = model.Name;
            project.Description = model.Description;

            var users = await _unitOfWork.User.GetAllAsync();
            var usersView = users.Select(user => new UserDetailDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });

            var roles = await _unitOfWork.Role.GetAllAsync();
            var rolesView = roles
                .Where(x => x.Name != Role.Admin.ToString())
                .OrderBy(x => x.Name)
                .Select(r => new ApplicationRoleDetailDTO
                {
                    Id = r.Id,
                    RoleName = r.Name,
                    Description = r.Description
                });

            model.Roles = rolesView;
            model.Users = usersView;

            var userRoles = _unitOfWork.Project.GetProjectUserRoles(project.Id);

            model.ProjectUsers = new List<ProjectUserDetailDTO>();

            foreach (var userRole in userRoles)
            {
                var user = users.FirstOrDefault(x => x.Id == userRole.UserId);
                var role = roles.FirstOrDefault(x => x.Id == userRole.RoleId);
                var projectUser = new ProjectUserDetailDTO
                {
                    ProjectId = project.Id,
                    UserId = userRole.UserId,
                    RoleId = userRole.RoleId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserRoleName = role.Name
                };

                model.ProjectUsers.Add(projectUser);
            }

            _unitOfWork.Project.Update(project);
            await _unitOfWork.SaveAsync();

            model.StatusMessage = $"{project.Name} project has been updated";


            return _mapper.Map<ProjectResponse>(model);
        }
    }
}
