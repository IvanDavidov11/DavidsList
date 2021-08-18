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
    public class Test_MyProfileController
    {
        [Fact]
        public void Test_MyProfileController_Index_ReturnsAViewModel_WithAMyProfileViewModel()
        {
            // Arrange
            var accountInteractor = new Mock<IAccountInteractor>();
            accountInteractor.Setup(repo => repo.GetMyProfileViewModel())
                .Returns(GetTestSessions_GetMarkedMovieViewModels());
            var controller = new MyProfileController(accountInteractor.Object);
            ClaimsPrincipal user = new SharedMethods().SimulateAuthentitactedUser();
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<MyProfileViewModel>(viewResult.ViewData.Model);
        }
        [Fact]
        public void Test_MyProfileController_Index_BlocksUserFromAccessingWhenUnauthenticated()
        {
            // Arrange
            var accountInteractor = new Mock<IAccountInteractor>();
            var controller = new MyProfileController(accountInteractor.Object);

            // Assert
            Assert.Throws<NullReferenceException>(() => controller.Index());
        }

        [Fact]
        public void Test_MyProfileController_Preferences_ReturnsAViewModel_WithAMyProfileViewModel()
        {
            // Arrange
            var accountInteractor = new Mock<IAccountInteractor>();
            accountInteractor.Setup(repo => repo.GetPreferencesModel())
                .Returns(GetTestSessions_GetPreferencesModel());
            var controller = new MyProfileController(accountInteractor.Object);
            ClaimsPrincipal user = new SharedMethods().SimulateAuthentitactedUser();
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            // Act
            var result = controller.Preferences();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<GenreViewModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());
        }
        [Fact]
        public void Test_MyProfileController_Preferences_BlocksUserFromAccessingWhenUnauthenticated()
        {
            // Arrange
            var accountInteractor = new Mock<IAccountInteractor>();
            var controller = new MyProfileController(accountInteractor.Object);

            // Assert
            Assert.Throws<NullReferenceException>(() => controller.Preferences());
        }

        private MyProfileViewModel GetTestSessions_GetMarkedMovieViewModels()
        {
            return new MyProfileViewModel
            {
                Email = "",
                ImageUrl = "",
                Introduction = "",
            };

        }

        private List<GenreViewModel> GetTestSessions_GetPreferencesModel()
        {
            var result = new List<GenreViewModel>();
            result.Add(new GenreViewModel
            {
                GenreType = "",
                Id = 1,
                IsPicked = false
            });
            result.Add(new GenreViewModel
            {
                GenreType = "",
                Id = 2,
                IsPicked = false
            }); result.Add(new GenreViewModel
            {
                GenreType = "",
                Id = 3,
                IsPicked = false
            });
            return result;
        }

    }
}
