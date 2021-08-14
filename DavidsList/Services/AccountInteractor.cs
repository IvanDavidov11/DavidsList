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

    public class AccountInteractor : IAccountInteractor
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly DavidsListDbContext data;
        private readonly INotyfService _notyf;
        public AccountInteractor(UserManager<User> userManager, SignInManager<User> signInManager, DavidsListDbContext db, INotyfService notyf)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.data = db;
            _notyf = notyf;
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
    }
}
