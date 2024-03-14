using FrybreadFusion.Controllers;
using FrybreadFusion.ViewModels;
using FrybreadFusion.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    // Constructor here initializes UserManager, SignInManager and roleManager
    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }




    // Display the user registration page
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    // Handle the user registration form submission
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Copy data from RegisterViewModel to IdentityUser
            var user = new AppUser { UserName = model.Email, Email = model.Email, FullName = model.FullName };
            var result = await _userManager.CreateAsync(user, model.Password);

            // If user is successfully created, sign-in the user using
            // SignInManager and redirect to index action of HomeController
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("index", "home");
            }
            // If there are any errors, add them to the ModelState object
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        // If we got this far, something failed, so redisplay the form
        return View(model);
    }
    // Display the user login page
    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }
    // Handle the user login form submission
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        // This doesn't count login failures towards account lockout
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            TempData["SuccessMessage"] = "You are now logged in.";
            // Upon successful login, redirect to the page user tried to access,
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        // If we got this far, something failed, display error message
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View(model);
    }

    // Handle the user logout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        // Sign out the user
        await _signInManager.SignOutAsync();
        // Redirect to the index page
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    public ViewResult AccessDenied()
    {
        return View();
    }
}
