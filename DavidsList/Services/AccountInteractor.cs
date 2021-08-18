namespace DavidsList.Services
{
    using DavidsList.Data;
    using DavidsList.Data.DbModels;
    using DavidsList.Models.FormModels;
    using DavidsList.Services.Interfaces;
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using DavidsList.Models.ViewModels;
    using DavidsList.Data.DbModels.ManyToManyTables;
    using Microsoft.EntityFrameworkCore;
    using System;
    using Microsoft.AspNetCore.Mvc;

    public class AccountInteractor : IAccountInteractor
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly DavidsListDbContext data;
        private readonly INotyfService _notyf;
        private readonly IUserService user;

        public AccountInteractor(UserManager<User> userManager, SignInManager<User> signInManager, DavidsListDbContext db, INotyfService notyf, IUserService userService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.user = userService;
            this.data = db;
            _notyf = notyf;
        }

        public void SetGenresForUser(int[] genres)
        {
            var username = user.GetUser().Identity.Name;
            var curUser = data.Users.Include(x=>x.UserGenres).First(x => x.UserName == username);
            curUser.UserGenres.Clear();
            foreach (var genre in genres)
            {
                    curUser.UserGenres.Add(new GenreUser
                    {
                        GenreId = genre,
                    });
            }
            curUser.FirstTimeLogginIn = false;
            data.SaveChanges();
            _notyf.Success("Your preferences have been updated...");
        }
        public bool CheckIfFirstTimeLogin(string username)
        {
            var curUser = data.Users.First(x => x.UserName == username);
            return curUser.FirstTimeLogginIn;
        }



        public async Task<Dictionary<string, string>> TryLoggingUserIn(LoginFormModel model)
        {
            var errors = new Dictionary<string, string>();
            var loggedUser = await this.userManager.FindByNameAsync(model.Username);

            if (loggedUser == null)
            {
                errors.Add(string.Empty, "Wrong Username or Password, please check them over...");
                return errors;
            }

            var passwordIsValid = await this.userManager.CheckPasswordAsync(loggedUser, model.Password);

            if (!passwordIsValid)
            {
                errors.Add(string.Empty, "Wrong Username or Password, please check them over...");
                return errors;
            }

            await this.signInManager.SignInAsync(loggedUser, true);
            var firstLogg = data.Users.FirstOrDefault(x => x.UserName == model.Username).FirstTimeLogginIn;
            _notyf.Success("Logged-in successfuly, redirecting to Home page.");
            return null;
        }


        public async Task<Dictionary<string, string>> TryRegisteringUser(RegisterFormModel model)
        {
            var errors = new Dictionary<string, string>();

            var registeredUser = new User
            {
                Email = model.Email,
                UserName = model.Username,
            };

            var anythingTaken = checkForAvaliabilty(model);
            if (anythingTaken.Count > 0)
            {
                return anythingTaken;
            }

            var result = await this.userManager.CreateAsync(registeredUser, model.Password);
            if (!result.Succeeded)
            {
                var failErrors = result.Errors.Select(x => x.Description);

                foreach (var error in failErrors)
                {
                    errors.Add("RegFailed", error.ToString());
                }
                return errors;
            }
            _notyf.Success("Registered successfuly, redirecting to Login page.");
            return null;
        }

        private Dictionary<string, string> checkForAvaliabilty(RegisterFormModel model)
        {
            var result = new Dictionary<string, string>();
            if (this.data.Users.FirstOrDefault(x => x.Email == model.Email) != null)
            {
                result.Add("EmailTaken", "This E-Mail is already taken. Please try again using another...");
            };
            if (this.data.Users.FirstOrDefault(x => x.NormalizedUserName == model.Username.ToUpper()) != null)
            {
                result.Add("UNameTaken", "This Username is already taken. Please try again using another...");
            };
            return result;
        }

        public List<GenreViewModel> GetPreferencesModel()
        {
            var username = user.GetUser().Identity.Name;
             var curUser = data.Users.Include(x => x.UserGenres).First(x => x.UserName == username);
            var result = new List<GenreViewModel>();
            foreach (var genre in data.Genres)
            {
                result.Add(new GenreViewModel
                {
                    Id = genre.Id,
                    GenreType = genre.GenreType,
                    IsPicked = curUser.UserGenres.FirstOrDefault(x => x.GenreId == genre.Id) != null ? true : false
                });
            }
            return result;
        }
        public List<GenreViewModel> GetPreferencesModel_Specific()
        {
            var result = new List<GenreViewModel>();
            foreach (var genre in data.Genres)
            {
                result.Add(new GenreViewModel
                {
                    Id = genre.Id,
                    GenreType = genre.GenreType,
                    IsPicked = false
                });
            }
            return result;
        }

        public MyProfileViewModel GetMyProfileViewModel()
        {
            var curUser = data.Users.FirstOrDefault(x => x.UserName == user.GetUser().Identity.Name);
            return new MyProfileViewModel
            {
                Email = curUser.Email,
                ImageUrl = curUser.ProfilePictureUrl == null ? "https://cdn.drawception.com/drawings/A4xPK14g50.png" : curUser.ProfilePictureUrl,
                Introduction = curUser.Introduction,
            };
        }

        public MyProfileViewModel SetMyProfileDetails(string intrd, string url)
        {
            if (intrd != null)
            {
                data.Users.FirstOrDefault(x => x.UserName == user.GetUser().Identity.Name).Introduction = intrd;
                _notyf.Success("Successfuly changed profile introdcution...");
            }
            if (url != null && Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                data.Users.FirstOrDefault(x => x.UserName == user.GetUser().Identity.Name).ProfilePictureUrl = url;
                _notyf.Success("Successfuly changed profile picture...");
            }
            data.SaveChanges();
            return GetMyProfileViewModel();
        }
    }
}
