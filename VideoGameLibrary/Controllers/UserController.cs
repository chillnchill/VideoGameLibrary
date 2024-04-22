using Griesoft.AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using VideoGameLibrary.Data.Models;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Web.Infrastructure.Extensions;
using VideoGameLibrary.Web.ViewModels.User;
using static VideoGameLibrary.Common.GeneralApplicationConstants;
using static VideoGameLibrary.Common.NotificationMessagesConstants;

namespace VideoGameLibrary.Controllers
{
	public class UserController : Controller
	{
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMemoryCache memoryCache;
        private readonly IUserService userService;

        public UserController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, 
            IMemoryCache memoryCache, IUserService userService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.memoryCache = memoryCache;
            this.userService = userService; 
        }

        //response caching is different because instead in the database, it caches into the browser
		[ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client, NoStore = false)]
		public async Task<IActionResult> All()
		{
			IEnumerable<UserViewModel> users = memoryCache.Get<IEnumerable<UserViewModel>>(UsersCacheKey);
			if (users == null)
			{
				users = await userService.AllAsync();

				MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
					.SetAbsoluteExpiration(TimeSpan
						.FromMinutes(UsersCacheDurationMinutes));

				memoryCache.Set(UsersCacheKey, users, cacheOptions);
			}

			return View(users);
		}

		[HttpGet]
		public async Task<IActionResult> Password()
		{
			if (!User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Login", "User");
			}

			ApplicationUser user = await userManager.FindByNameAsync(User.Identity.Name);
			if (user == null)
			{
				return NotFound();
			}

            ChangePasswordViewModel model = new ChangePasswordViewModel();
			return View(model);
		}

		[HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Password(ChangePasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			ApplicationUser user = await userManager.FindByNameAsync(User.Identity!.Name);
			if (user == null)
			{
				return NotFound(); 
			}

			IdentityResult changePasswordResult = await userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
			if (!changePasswordResult.Succeeded)
			{
				foreach (var error in changePasswordResult.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
				return View(model); 
			}

			TempData[SuccessMessage] = "Your password has been changed successfully!";
			return RedirectToAction("Profile", "User"); 
		}

		[HttpGet]
        public async Task<IActionResult> Profile()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "User");
            }

			ApplicationUser user = await userManager.FindByNameAsync(User.Identity!.Name);
			user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

			ProfileViewModel model = new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Nickname = user.Nickname
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "User");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors })
                    .ToArray();
                return View(model);
            }

            ApplicationUser user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Nickname = model.Nickname;

            IdentityResult result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

			TempData[SuccessMessage] = "Your Profile has been updated successfully!";
			return RedirectToAction("Profile");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateRecaptcha(Action = nameof(Register),
            ValidationFailedAction = ValidationFailedAction.ContinueRequest)]
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Nickname = model.Nickname,
            };

            await userManager.SetEmailAsync(user, model.Email);
            await userManager.SetUserNameAsync(user, model.Email);

            IdentityResult result =
                await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }

            await signInManager.SignInAsync(user, false);
            memoryCache.Remove(UsersCacheKey);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            //preventing a double login attempt
            //clears cookies from previous log
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            LoginFormModel model = new LoginFormModel()
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result =
                await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
            {
                TempData[ErrorMessage] =
                    "An error occurred during the login attempt! Please try again later or contact an administrator.";

                return View(model);
            }

			return Redirect(model.ReturnUrl ?? "/Game/All");
        }
    }
}
