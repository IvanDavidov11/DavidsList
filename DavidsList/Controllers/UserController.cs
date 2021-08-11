namespace DavidsList.Controllers
{
    using System.Linq;
    using DavidsList.Data;
    using System.Threading.Tasks;
    using DavidsList.Data.DbModels;
    using Microsoft.AspNetCore.Mvc;
    using DavidsList.Models.FormModels;
    using Microsoft.AspNetCore.Identity;
    using AspNetCoreHero.ToastNotification.Abstractions;
    using static Data.DataConstants;

    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly DavidsListDbContext data;
        private readonly INotyfService _notyf;


        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, DavidsListDbContext db, INotyfService notyf)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.data = db;
            _notyf = notyf;
        }

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");

            }

            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var registeredUser = new User
            {
                Email = model.Email,
                UserName = model.Username,
            };
            var anythingTaken = checkForAvaliabilty(model);
            if (anythingTaken)
            {
                return View(model);
            }

            var result = await this.userManager.CreateAsync(registeredUser, model.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                foreach (var error in errors)
                {
                    ModelState.AddModelError("RegFailed", error);
                }
                return View(model);
            }
            _notyf.Success("Registered successfuly, redirecting to Login page.");
            return RedirectToAction("Login", "User");
        }

        private bool checkForAvaliabilty(RegisterFormModel model)
        {
            var result = false;
            if (this.data.Users.FirstOrDefault(x => x.Email == model.Email) != null)
            {
                ModelState.AddModelError("EmailTaken", "This E-Mail is already taken. Please try again using another...");
                result = true;
            };
            if (this.data.Users.FirstOrDefault(x => x.NormalizedUserName == model.Username.ToUpper()) != null)
            {
                ModelState.AddModelError("UNameTaken", "This Username is already taken. Please try again using another...");
                result = true;
            };
            return result;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
            if (model.Username == null || model.Password == null)
            {
                return View(model);
            }
            var loggedUser = await this.userManager.FindByNameAsync(model.Username);

            if (loggedUser == null)
            {
                ModelState.AddModelError(string.Empty, "Wrong Username or Password, please check them over...");
                return View(model);
            }

            var passwordIsValid = await this.userManager.CheckPasswordAsync(loggedUser, model.Password);

            if (!passwordIsValid)
            {
                ModelState.AddModelError(string.Empty, "Wrong Username or Password, please check them over...");
                return View(model);
            }

            await this.signInManager.SignInAsync(loggedUser,true);
            _notyf.Success("Logged-in successfuly, redirecting to Home page.");
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
      
    }
   
}
