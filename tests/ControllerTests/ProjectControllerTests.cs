using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Data.Models.Identity;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Mvc.Controllers;
using TremendBoard.Mvc.Models.ProjectViewModels;

namespace ControllerTests
{
    public class ProjectControllerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public ProjectControllerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWork = _unitOfWorkMock.Object;
        }

        #region Index

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public async Task Index_ReturnViewResult(int projectsToAdd)
        {
            // Arrange
            var dummyProjectsList = new List<Project>();
            for(int counter = 0; counter < projectsToAdd; counter++) 
            {
                dummyProjectsList.Add(new Project()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = Guid.NewGuid().ToString()
                });
            }

            ProjectController projectController = new ProjectController(_unitOfWork);

            _unitOfWorkMock.Setup(action => action.Project.GetAllAsync()).ReturnsAsync(dummyProjectsList);
            // Act
            var result = await projectController.Index();
            // Assert
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            var expectedListCount = dummyProjectsList.Count();
            var actualListCount = (viewResult.Model as ProjectIndexViewModel)
                .Projects.Count();

            Assert.Equal(actualListCount, actualListCount);
        }

        #endregion

        #region Create 
        [Fact]
        public async Task Create_ValidModel_ReturnRedirectToActionResult()
        {
            // Arrange
            var dummyModel = new ProjectDetailViewModel();

            ProjectController projectController = new ProjectController(_unitOfWork);
            _unitOfWorkMock.Setup(action => action.Project.AddAsync(It.IsAny<Project>())).Verifiable();
            // Act
            var result = await projectController.Create(dummyModel);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task Create_InalidModel_ReturnViewResult()
        {
            // Arrange
            var dummyModel = new ProjectDetailViewModel();

            ProjectController projectController = new ProjectController(_unitOfWork);
            projectController.ModelState.AddModelError("Error", "Test error");

            _unitOfWorkMock.Setup(action => action.Project.AddAsync(It.IsAny<Project>())).Verifiable();
            // Act
            var result = await projectController.Create(dummyModel);

            // Assert
            Assert.IsType<ViewResult>(result);
        }
        #endregion

        #region Edit
        [Fact]
        public async Task Edit_projectNotFound_ThrowApplicationException()
        {
            // Arrange
            string projectId = Guid.NewGuid().ToString();
            Project? nullProject = null;

            _unitOfWorkMock.Setup(action => action.Project.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(nullProject);

            ProjectController projectController = new ProjectController(_unitOfWork);

            // Assert
            await Assert.ThrowsAsync<ApplicationException>(async () =>
            {
                // Act
                await projectController.Edit(projectId);
            });
        }

        [Fact]
        public async Task Edit_projectFound_ReturnViewResult()
        {
            // Arrange
            string projectId = Guid.NewGuid().ToString();
            Project dummyProject = new Project();

            _unitOfWorkMock.Setup(action => action.Project.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(dummyProject);

            var applicationUser = new List<ApplicationUser>();
            _unitOfWorkMock.Setup(action => action.User.GetAllAsync())
                .ReturnsAsync(applicationUser);

            var roles = new List<ApplicationRole>();
            _unitOfWorkMock.Setup(action => action.Role.GetAllAsync())
                .ReturnsAsync(roles);

            var userRoles = new List<ApplicationUserRole>();
            _unitOfWorkMock.Setup(action => action.Project.GetProjectUserRoles(It.IsAny<string>()))
                .Returns(userRoles);

            ProjectController projectController = new ProjectController(_unitOfWork);

            // Act
            var result = await projectController.Edit(projectId);

            // Assert
            Assert.IsType<ViewResult>(result);
        }
        #endregion
    }
}
