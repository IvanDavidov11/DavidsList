namespace DavidsList.Test.Controller
{
    using AspNetCoreHero.ToastNotification.Abstractions;
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

    public class Test_RandomMovieGeneratorController
    {
        [Fact]
        public void Test_RandomMovieGeneratorController_Index_ReturnsAViewModel_WithAnIEnumerableOfGenreViewModel()
        {
            // Arrange
            var apiConnectorMock = new Mock<IGetInformationFromApi>();
            var notifMock = new Mock<INotyfService>();
            var accountInteractor = new Mock<IAccountInteractor>();
            accountInteractor.Setup(repo => repo.GetPreferencesModel_Specific())
                .Returns(GetTestSessions_PreferenceModels());
            var controller = new RandomMovieGeneratorController(apiConnectorMock.Object, notifMock.Object, accountInteractor.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<GenreViewModel>>(
                 viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());
        }

        [Fact]
        public void Test_RandomMovieGeneratorController_Surprise_RedirectsToMovieDetails_WithCorrectMoviePathStringFormat()
        {
            // Arrange
            var notifMock = new Mock<INotyfService>();
            var accountInteractor = new Mock<IAccountInteractor>();
            var apiConnectorMock = new Mock<IGetInformationFromApi>();
            apiConnectorMock.Setup(repo => repo.GetRandomMoviePath_Surprise())
                .Returns(GetTestSessions_RandomMoviePath());
            var controller = new RandomMovieGeneratorController(apiConnectorMock.Object, notifMock.Object, accountInteractor.Object);

            // Act
            var result = (RedirectToActionResult)controller.Surprise();

            // Assert
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("MovieDetails", result.ControllerName);

            var resultFromService = GetTestSessions_RandomMoviePath();
            Assert.Matches(@"[t]{2}[0-9]*", resultFromService);
        }

        [Fact]
        public void Test_RandomMovieGeneratorController_Preferred_RedirectsToMovieDetails_WithCorrectMoviePathStringFormat()
        {
            // Arrange
            var notifMock = new Mock<INotyfService>();
            var accountInteractor = new Mock<IAccountInteractor>();
            var apiConnectorMock = new Mock<IGetInformationFromApi>();
            apiConnectorMock.Setup(repo => repo.GetRandomMoviePath_Preferred())
                .Returns(GetTestSessions_RandomMoviePath());
            apiConnectorMock.Setup(repo => repo.CheckIfUserHasFavouritedGenre()).Returns(true);
            var controller = new RandomMovieGeneratorController(apiConnectorMock.Object, notifMock.Object, accountInteractor.Object);


            ClaimsPrincipal user = new SharedMethods().SimulateAuthentitactedUser();
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            // Act
            var result = (RedirectToActionResult)controller.Preferred();

            // Assert
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("MovieDetails", result.ControllerName);

            var resultFromService = GetTestSessions_RandomMoviePath();
            Assert.Matches(@"[t]{2}[0-9]*", resultFromService);
        }
        [Fact]
        public void Test_RandomMovieGeneratorController_Preferred_BlocksUserFromAccessingWhenUnauthenticated()
        {
            // Arrange
            var notifMock = new Mock<INotyfService>();
            var accountInteractor = new Mock<IAccountInteractor>();
            var apiConnectorMock = new Mock<IGetInformationFromApi>();
          
            var controller = new RandomMovieGeneratorController(apiConnectorMock.Object, notifMock.Object, accountInteractor.Object);

            // Assert
            Assert.Throws<NullReferenceException>(() => controller.Preferred());
        }

        [Fact]
        public void Test_RandomMovieGeneratorController_Preferred_ReturnsViewIfUserIsHasNoFavourites()
        {
            // Arrange
            var notifMock = new Mock<INotyfService>();
            var accountInteractor = new Mock<IAccountInteractor>();
            var apiConnectorMock = new Mock<IGetInformationFromApi>();
            apiConnectorMock.Setup(repo => repo.CheckIfUserHasFavouritedGenre()).Returns(false);
            var controller = new RandomMovieGeneratorController(apiConnectorMock.Object, notifMock.Object, accountInteractor.Object);
            ClaimsPrincipal user = new SharedMethods().SimulateAuthentitactedUser();
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            // Act
            var result = (RedirectToActionResult)controller.Preferred();

            // Assert
            Assert.Equal("RandomMovieGenerator", result.ControllerName);
            Console.WriteLine();
        }


        [Fact]
        public void Test_RandomMovieGeneratorController_Specific_RedirectsToMovieDetails_WithCorrectMoviePathStringFormat_WithCorrectTypeOfInput()
        {
            // Arrange
            var notifMock = new Mock<INotyfService>();
            var accountInteractor = new Mock<IAccountInteractor>();
            var apiConnectorMock = new Mock<IGetInformationFromApi>();
            apiConnectorMock.Setup(repo => repo.GetRandomMoviePath_Specific(1))
                .Returns(GetTestSessions_RandomMoviePath());
            var controller = new RandomMovieGeneratorController(apiConnectorMock.Object, notifMock.Object, accountInteractor.Object);

            // Act
            int testMovieId = 1;
            var result = (RedirectToActionResult)controller.Specific(testMovieId);

            // Assert
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("MovieDetails", result.ControllerName);
            Assert.IsType<int>(testMovieId);

            var resultFromService = GetTestSessions_RandomMoviePath();
            Assert.Matches(@"[t]{2}[0-9]*", resultFromService);
        }


        private string GetTestSessions_RandomMoviePath()
        {
            return "tt00001";
        }

        private List<GenreViewModel> GetTestSessions_PreferenceModels()
        {
            var result = new List<GenreViewModel>();
            result.Add(new GenreViewModel
            {
                GenreType = "test",
                IsPicked = false,
                Id = 1,
            });
            result.Add(new GenreViewModel
            {
                GenreType = "test2",
                IsPicked = false,
                Id = 2,
            }); result.Add(new GenreViewModel
            {
                GenreType = "test3",
                IsPicked = false,
                Id = 3,
            });


            return result;
        }
    }
}
