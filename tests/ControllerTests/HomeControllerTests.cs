using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Mvc.Controllers;

namespace ControllerTests
{
    public class HomeControllerTests
    {
        private readonly IDateTime _dateTime;
        private readonly Mock<IDateTime> _dateTimeMock;

        public HomeControllerTests()
        {
            _dateTimeMock = new Mock<IDateTime>();
            _dateTime = _dateTimeMock.Object;
        }

        #region Index

        [Theory]
        [MemberData(nameof(IndexData))]
        public void Index_ReturnViewResult(DateTime input, string output)
        {
            _dateTimeMock.Setup(action => action.Now).Returns(input);

            var homeController = new HomeController(_dateTime);

            var result = homeController.Index();
            Assert.IsType<ViewResult>(result);

            var resultView = result as ViewResult;
            Assert.Equal(resultView.ViewData["Message"], output);

        }

        public static IEnumerable<object[]> IndexData =>
            new List<object[]>
            {
                new object[] {DateTime.Parse("01/01/2001 06:00:00"), "It's morning here - Good Morning!"},
                new object[] {DateTime.Parse("01/01/2001 13:00:00"), "It's afternoon here - Good Afternoon!"},
                new object[] {DateTime.Parse("01/01/2001 18:00:00"), "It's evening here - Good Evening!"},
            };

        #endregion
    }
}
