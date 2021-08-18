
namespace DavidsList.Test.Controller
{
    using DavidsList.Controllers;
    using DavidsList.Models.ViewModels;
    using DavidsList.Services.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Xunit;
    public class Test_MyMoviesController
    {
        [Fact]
        public void Test_MyMoviesController_Favourited_ReturnsAViewModel_WithAnIEnumerableMarkedMovieViewModel()
        {
            // Arrange
            var dbInteractorMock = new Mock<IDatabaseInteractor>();
            dbInteractorMock.Setup(repo => repo.GetMarkedMovieViewModel_Favourited())
                .Returns(GetTestSessions_GetMarkedMovieViewModels());
            var controller = new MyMoviesController(dbInteractorMock.Object);
            ClaimsPrincipal user = new SharedMethods().SimulateAuthentitactedUser();
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            // Act
            var result = controller.Favourites();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<MarkedMovieViewModel>>(
                 viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());
        }
        [Fact]
        public void Test_MyMoviesController_Favourited_BlocksUserFromAccessingWhenUnauthenticated()
        {
            // Arrange
            var dbInteractorMock = new Mock<IDatabaseInteractor>();
            var controller = new MyMoviesController(dbInteractorMock.Object);

            // Assert
            Assert.Throws<NullReferenceException>(() => controller.Favourites());
        }

        [Fact]
        public void Test_MyMoviesController_Seen_ReturnsAViewModel_WithAnIEnumerableMarkedMovieViewModel()
        {
            // Arrange
            var dbInteractorMock = new Mock<IDatabaseInteractor>();
            dbInteractorMock.Setup(repo => repo.GetMarkedMovieViewModel_Seen())
                .Returns(GetTestSessions_GetMarkedMovieViewModels());
            var controller = new MyMoviesController(dbInteractorMock.Object);
            ClaimsPrincipal user = new SharedMethods().SimulateAuthentitactedUser();
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            // Act
            var result = controller.Seen();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<MarkedMovieViewModel>>(
                 viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());
        }
        [Fact]
        public void Test_MyMoviesController_Seen_BlocksUserFromAccessingWhenUnauthenticated()
        {
            // Arrange
            var dbInteractorMock = new Mock<IDatabaseInteractor>();
            var controller = new MyMoviesController(dbInteractorMock.Object);

            // Assert
            Assert.Throws<NullReferenceException>(() => controller.Seen());
        }

        [Fact]
        public void Test_MyMoviesController_Flagged_ReturnsAViewModel_WithAnIEnumerableMarkedMovieViewModel()
        {
            // Arrange
            var dbInteractorMock = new Mock<IDatabaseInteractor>();
            dbInteractorMock.Setup(repo => repo.GetMarkedMovieViewModel_Flagged())
                .Returns(GetTestSessions_GetMarkedMovieViewModels());
            var controller = new MyMoviesController(dbInteractorMock.Object);
            ClaimsPrincipal user = new SharedMethods().SimulateAuthentitactedUser();
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            // Act
            var result = controller.Flagged();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<MarkedMovieViewModel>>(
                 viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());
        }
        [Fact]
        public void Test_MyMoviesController_Flagged_BlocksUserFromAccessingWhenUnauthenticated()
        {
            // Arrange
            var dbInteractorMock = new Mock<IDatabaseInteractor>();
            var controller = new MyMoviesController(dbInteractorMock.Object);

            // Assert
            Assert.Throws<NullReferenceException>(() => controller.Flagged());
        }

        private List<MarkedMovieViewModel> GetTestSessions_GetMarkedMovieViewModels()
        {
            var result = new List<MarkedMovieViewModel>();
            result.Add(new MarkedMovieViewModel
            {
                Buttons = null,
                ImageUrl = "test",
                MoviePath = "test",
                ReleaseDate = "1",
                Title = "test"
            });
            result.Add(new MarkedMovieViewModel
            {
                Buttons = null,
                ImageUrl = "test2",
                MoviePath = "test2",
                ReleaseDate = "2",
                Title = "test2"
            }); result.Add(new MarkedMovieViewModel
            {
                Buttons = null,
                ImageUrl = "test3",
                MoviePath = "test3",
                ReleaseDate = "3",
                Title = "test3"
            });
            return result;
        }

    }
}
