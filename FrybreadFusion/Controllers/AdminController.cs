using FrybreadFusion.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
// This controller is used to manage users and roles
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    // This method is used to display the UserManagementView
    public async Task<IActionResult> UserManagementView()
    {
        var users = _userManager.Users.ToList();
        var model = new UserManagementViewModel
        {
            Users = await Task.WhenAll(users.Select(async user => new UserDetailsViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                Roles = await _userManager.GetRolesAsync(user)
            }))
        };

        return View(model);
    }
    // This method is used to display the RoleManagementView
    public IActionResult RoleManagementView()
    {
        var roles = _roleManager.Roles.ToList();
        var model = new RoleViewModel
        {
            Roles = roles.Select(role => role.Name).ToList()
        };

        return View(model);
    }

    // This method is used to create a new role
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            // User not found, redirect to management view with an error message
            TempData["ErrorMessage"] = "User not found.";
            return RedirectToAction("UserManagementView");
        }

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            // User successfully deleted, redirect with a success message
            TempData["SuccessMessage"] = "User successfully deleted.";
        }
        else
        {
            // Deletion failed, redirect with an error message
            TempData["ErrorMessage"] = "Failed to delete user. " + result.Errors.FirstOrDefault()?.Description;
        }
        return RedirectToAction("UserManagementView");
    }
    // This method is used to create a new role
    public async Task<IActionResult> AddToAdmin(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            TempData["ErrorMessage"] = "User not found.";
            return RedirectToAction("UserManagementView");
        }
        // Add user to Admin role
        var result = await _userManager.AddToRoleAsync(user, "Admin");
        if (result.Succeeded)
        {
            TempData["SuccessMessage"] = "User successfully added to Admin.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to add user to Admin. " + result.Errors.FirstOrDefault()?.Description;
        }
        return RedirectToAction("UserManagementView");
    }
// This method is used to remove a user from the Admin role
    public async Task<IActionResult> RemoveFromAdmin(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            TempData["ErrorMessage"] = "User not found.";
            return RedirectToAction("UserManagementView");
        }

        var result = await _userManager.RemoveFromRoleAsync(user, "Admin");
        if (result.Succeeded)
        {
            TempData["SuccessMessage"] = "User successfully removed from Admin.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to remove user from Admin. " + result.Errors.FirstOrDefault()?.Description;
        }
        return RedirectToAction("UserManagementView");
    }

    public async Task<IActionResult> DeleteRole(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            TempData["ErrorMessage"] = "Role not found.";
            return RedirectToAction("UserManagementView");
        }

        var result = await _roleManager.DeleteAsync(role);
        if (result.Succeeded)
        {
            TempData["SuccessMessage"] = "Role successfully deleted.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to delete role. " + result.Errors.FirstOrDefault()?.Description;
        }
        return RedirectToAction("UserManagementView");
    }

}
