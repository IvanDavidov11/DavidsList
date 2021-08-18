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
    public class Test_MovieDetailsController
    {
        [Fact]
        public void Test_MovieDetailsController_Index_ReturnsAViewModel_WithAMovieDetailsViewModel_AndCorrectMoviePath()
        {
            // Arrange
            var path = "tt00001";
            var apiConnector = new Mock<IGetInformationFromApi>();
            apiConnector.Setup(repo => repo.GetSpecificMovieDetails(path).Result)
                .Returns(GetTestSessions_GetMarkedMovieViewModels());
            var controller = new MovieDetailsController(apiConnector.Object);
            // Act
            var result = controller.Index(path);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Matches(@"[t]{2}[0-9]*", path);
            Assert.IsAssignableFrom<MovieDetailsViewModel>(viewResult.ViewData.Model);
        }

        private MovieDetailsViewModel GetTestSessions_GetMarkedMovieViewModels()
        {
            return new MovieDetailsViewModel
            {
                ShortPlot = "",
                Buttons = null,
                Genres = new(),
                ImgUrl = "",
                LongPlot = "",
                MoviePath = "",
                Raiting = 10,
                RaitingCount = 1,
                ReleaseDate = "",
                RunningTimeInMinutes = 1,
                Title = ""
            };
        }
    }
}
