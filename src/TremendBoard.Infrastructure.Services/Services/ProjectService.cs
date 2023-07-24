using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.DTO.ProjectViewModels;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Infrastructure.Services.Services;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class ProjectService : IProjectService
    {

        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task CreateProject(ProjectDetailViewModel model)
        {
            await _unitOfWork.Project.AddAsync(new Project
            {
                Name = model.Name,
                Description = model.Description,
                CreatedDate = DateTime.Now,
                ProjectStatus = model.ProjectStatus,
                Deadline = model.ProjectDeadline
            });

            await _unitOfWork.SaveAsync();

        }

        
    }
}
