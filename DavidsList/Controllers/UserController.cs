namespace DavidsList.Controllers
{
    using DavidsList.Data;
    using DavidsList.Data.DbModels;
    using DavidsList.Models.FormModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly DavidsListDbContext data;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, DavidsListDbContext db)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.data = db;
        }

        public IActionResult Register()
        {
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
            var anythingTaken = false;
            if (this.data.Users.FirstOrDefault(x => x.Email == model.Email) != null)
            {
                ModelState.AddModelError("EmailTaken", "This E-Mail is already taken. Please try again using another...");
                anythingTaken = true;
            };
            if (this.data.Users.FirstOrDefault(x => x.NormalizedUserName == model.Username.ToUpper()) != null)
            {
                ModelState.AddModelError("UNameTaken", "This Username is already taken. Please try again using another...");
                anythingTaken = true;
            };
            if (!anythingTaken)
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

            return RedirectToAction("Login", "User");
        }
        
        public IActionResult Login()
        {
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
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
      
    }
   
}
