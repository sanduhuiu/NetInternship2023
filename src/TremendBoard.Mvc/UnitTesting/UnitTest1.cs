using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Serilog;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Infrastructure.Services.Services;
using TremendBoard.Mvc.Controllers;
using Xunit;
using TremendBoard.Mvc.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TremendBoard.Infrastructure.Data.Models.Identity;
using Microsoft.AspNetCore.Http;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Mvc.Models.ProjectViewModels;

namespace UnitTesting
{
    public class UnitTest1
    {
        [Fact]
        public void TestTimeServiceIsCorrect()
        {
            DateTime expected = DateTime.Now;

            TimeService timeService = new TimeService();
            DateTime actual = timeService.GetCurrentTime();

            Assert.Equal(expected, actual, TimeSpan.FromSeconds(1));

        }

        [Fact]
        public void Index_ShouldReturnViewResult()
        {
            // Arrange
            var dateTimeMock = new Mock<IDateTime>();
            var timeService1Mock = new Mock<ITimeService>();
            var timeService2Mock = new Mock<ITimeService>();

            var controller = new HomeController(dateTimeMock.Object, timeService1Mock.Object, timeService2Mock.Object);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
        private ProjectController CreateProjectController(IUnitOfWork unitOfWork)
        {
            // Create the ProjectController instance with the mock IUnitOfWork dependency
            var controller = new ProjectController(unitOfWork);

            // Add any additional setup for the controller, e.g., setting TempData, if needed.
            // For this example, we're not using TempData, so there's no setup required.

            return controller;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public async Task Index_ShouldReturnViewWithCorrectProjects(int projectCount)
        {
            // Arrange
            var projects = Enumerable.Range(1, projectCount).Select(i => new Project
            {
                Id = i.ToString(),
                Name = $"Project {i}",
                Description = $"Description {i}"
            }).ToList();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uow => uow.Project.GetAllAsync()).ReturnsAsync(projects);

            var controller = CreateProjectController(unitOfWorkMock.Object);

            // Act
            var result = await controller.Index();
            // Assert
            Assert.IsType<ViewResult>(result);
            var viewResult = (ViewResult)result;
            var model = viewResult.Model as ProjectIndexViewModel;
            Assert.NotNull(model);
            Assert.Equal(projectCount, model.Projects.Count());
            foreach (var project in projects)
            {
                Assert.Contains(model.Projects, p => p.Id == project.Id && p.Name == project.Name && p.Description == project.Description);
            }


        }

        [Fact]
        public async Task Create_InvalidModel_ShouldReturnViewWithModelError()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var controller = CreateProjectController(unitOfWorkMock.Object);
            controller.ModelState.AddModelError("Name", "The Name field is required.");

            var invalidModel = new ProjectDetailViewModel();

            // Act
            var result = await controller.Create(invalidModel);

            // Assert
            Assert.IsType<ViewResult>(result);
            var viewResult = (ViewResult)result;
            Assert.Equal(invalidModel, viewResult.Model); // Check if the returned model matches the input model
        }




    }
}