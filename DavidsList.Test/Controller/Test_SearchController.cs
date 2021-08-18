namespace DavidsList.Test.Controller
{
    using DavidsList.Controllers;
    using DavidsList.Models.ViewModels;
    using DavidsList.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    public class Test_SearchController
    {
        [Fact]
        public void Test_SearchController_ReturnsAViewModel_WithAListOfSearchResultViewModel()
        {
            // Arrange
            var apiConnectorMock = new Mock<IGetInformationFromApi>();
            apiConnectorMock.Setup(repo => repo.GetSearchResultModel("test"))
                .ReturnsAsync(GetTestSessions_SearchResult());
            var controller = new SearchController(apiConnectorMock.Object);

            // Act
            var result = controller.Result("test");
             
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<SearchResultsViewModel>>(
                 viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
        [Fact]
        public void Test_SearchController_IfSearchQueryIsEmptyRedirectToReferrer()
        {
            // Arrange
            var apiConnectorMock = new Mock<IGetInformationFromApi>();
            var controller = new SearchController(apiConnectorMock.Object);

            // Assert
            Assert.Throws<NullReferenceException>(() => controller.Result(null));
        }

        private List<SearchResultsViewModel> GetTestSessions_SearchResult()
        {
            var result = new List<SearchResultsViewModel>();
            result.Add(new SearchResultsViewModel
            {
                MoviePath = "tt0000",
                Title = "test",
                ImgUrl = "test",
                Buttons = null,
                Year = 2100,
            });
            result.Add(new SearchResultsViewModel
            {
                MoviePath = "tt0001",
                Title = "test1",
                ImgUrl = "test1",
                Buttons = null,
                Year = 2100,
            });
            return result;
        }
    }
}
