using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PrinceQ.Models.Entities;
using PrinceQ.Models.ViewModel;
using PrinceQ.Utility;
using System.Security.Claims;

namespace PrinceQueuing.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            try
            {
                if (User.Identity!.IsAuthenticated)
                {
                    string? userId =  User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var user = await _userManager.FindByIdAsync(userId!);

                    if (user != null)
                    {
                        var roles = await _userManager.GetRolesAsync(user);

                        if (roles.Contains(SD.Role_Register))
                        {
                            return RedirectToAction("Home", "RegisterPersonnel");
                        }
                        else if (roles.Contains(SD.Role_Clerk))
                        {
                            return RedirectToAction("Serving", "Clerk");
                        }
                        else if (roles.Contains(SD.Role_Admin))
                        {
                            return RedirectToAction("Dashboard", "Admin");
                        }
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                return View(ex);
            }

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                // Login
                var result = await _signInManager.PasswordSignInAsync(model.Username!, model.Password!, false, false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Username!);
                    var roles = await _userManager.GetRolesAsync(user!);

                    if (roles.Contains(SD.Role_Register))
                    {
                        return RedirectToAction("Home", "RegisterPersonnel");
                    }

                    else if (roles.Contains(SD.Role_Clerk))
                    {
                        return RedirectToAction("Serving", "Clerk");
                    }
                    else if (roles.Contains(SD.Role_Admin))
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
                }

                ModelState.AddModelError("", "Invalid login attempt");
            }

            return View(model);
        }



        public IActionResult Register()
        {

            // Create roles if they are not created
            if (!_roleManager.RoleExistsAsync(SD.Role_Clerk).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Clerk)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Register)).GetAwaiter().GetResult();
            }

            var roleList = _roleManager.Roles.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Name
            });

            var model = new RegisterVM
            {
                RoleList = roleList
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                User user = new()
                {
                    UserName = model.UserName,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (!String.IsNullOrEmpty(model.Role))
                    {
                        await _userManager.AddToRoleAsync(user, model.Role);
                    }

                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }


        public IActionResult AccessDenied()
        {
            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
