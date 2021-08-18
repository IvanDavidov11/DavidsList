namespace DavidsList.Test.Controller
{
    using DavidsList.Controllers;
    using DavidsList.Models.ViewModels;
    using DavidsList.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    public class Test_TopRatedMoviesController
    {
        [Fact]
        public void Test_TopRatedMoviesController_ReturnsAViewModel_WithAListOfCorrectViewModel()
        {
            // Arrange
            var apiConnectorMock = new Mock<IGetInformationFromApi>();
            apiConnectorMock.Setup(repo => repo.GetMoviesInParallel_MostRated())
                .ReturnsAsync(GetTestSessions_TopRated());
            var controller = new TopRatedMoviesController(apiConnectorMock.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<MovieQuickShowcaseViewModelWithRaiting>>(
                 viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        private List<MovieQuickShowcaseViewModelWithRaiting> GetTestSessions_TopRated()
        {
            var result = new List<MovieQuickShowcaseViewModelWithRaiting>();
            result.Add(new MovieQuickShowcaseViewModelWithRaiting
            {
                MoviePath = "tt0000",
                Raiting = 10,
                Title = "test",
                ImgUrl = "test",
                Buttons = null,
                Year = 2100,
            });
            result.Add(new MovieQuickShowcaseViewModelWithRaiting
            {
                MoviePath = "tt0001",
                Raiting = 10,
                Title = "test1",
                ImgUrl = "test1",
                Buttons = null,
                Year = 2100,
            });
            return result;
        }

    }
}
