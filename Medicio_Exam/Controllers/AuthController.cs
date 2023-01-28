using Business.Utilities;
using Business.ViewModels.Auth;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Medicio_Exam.Controllers
{

    /// Username:lalala, Password:Newpass123.
    /// Username:admin, Password:Newpass123. 

    [Authorize(Roles="Admin")]
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);
            AppUser user = new()
            {
                Fullname = registerVM.Fullname,
                UserName = registerVM.Username,
                Email = registerVM.Email,
            };


            var identityResult = await _userManager.CreateAsync(user, registerVM.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View(registerVM);
                }
            }
            await _userManager.AddToRoleAsync(user, EnumRole.Admin.ToString());
            return RedirectToAction("Index", "Home");
        }


        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View();
            var user = await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail);
            if (user is null)
            {
                user = await _userManager.FindByNameAsync(loginVM.UsernameOrEmail);
                if (user == null)
                {
                    ModelState.AddModelError("", "Username/Email or Password is incorrect");
                    return View(loginVM);
                }
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, true);
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Username/Email or Password is incorrect");
                return View(loginVM);
            }
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Try again to sign in later");
                return View(loginVM);
            }
            return RedirectToAction("Index", "Home");
        }


        public async Task<object> CreateRole()
        {
            foreach (var role in Enum.GetValues(typeof(EnumRole)))
            {
                var exists = await _roleManager.RoleExistsAsync(role.ToString());
                if (!exists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role.ToString()));
                }
            }
            return Json("ok");
        }
    }
}
