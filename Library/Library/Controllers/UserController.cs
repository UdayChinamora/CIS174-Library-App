using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Library.Models;
using System.Data;
using System.Threading.Tasks;

namespace Library.Controllers
{
    
    [Authorize(Roles ="Admin")]    
    public class UserController : Controller
    {
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<User> userMngr,
            RoleManager<IdentityRole> roleMngr)
        {
            _userManager = userMngr;
            _roleManager = roleMngr;
        }

        public async Task<IActionResult> AllUsers()
        {
            List<User> users = new List<User>();
            foreach (User user in _userManager.Users)
            {
                user.RoleNames = (List<string>)await _userManager.GetRolesAsync(user);
                users.Add(user);
            }
            UserViewModel model = new UserViewModel
            {
                Users = users,
                Roles = _roleManager.Roles
            };

            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id); if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user); if (!result.Succeeded)
                {
                    string errorMessage = "";
                    foreach (IdentityError error in result.Errors)
                    {
                        errorMessage += error.Description + " | ";
                    }
                    TempData["message"] = errorMessage;
                }
            }
            return RedirectToAction("AllUsers");
        }

        [HttpPost]
        public async Task<IActionResult> AddToAdmin(string id)
        {
            IdentityRole adminRole = await _roleManager.FindByNameAsync("Admin");
            if (adminRole == null)
            {
                TempData["message"] = "Admin role does not exist. " + "Click 'Create Admin Role' button to create it.";
            }
            else
            {
                User user = await _userManager.FindByIdAsync(id);
                await _userManager.AddToRoleAsync(user, adminRole.Name);
            }
            return RedirectToAction("AllUsers");
        }
        [HttpPost]
        public async Task<IActionResult> AddToLibrarian(string id)
        {
            IdentityRole librarianRole = await _roleManager.FindByNameAsync("Librarian");
            if (librarianRole == null)
            {
                TempData["message"] = "Librarian role does not exist. " + "Click 'Create Librarian Role' button to create it.";
            }
            else
            {
                User user = await _userManager.FindByIdAsync(id);
                await _userManager.AddToRoleAsync(user, librarianRole.Name);
            }
            return RedirectToAction("AllUsers");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromAdmin(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            await _userManager.RemoveFromRoleAsync(user, "Admin");
            return RedirectToAction("AllUsers");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromLibrarian(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            await _userManager.RemoveFromRoleAsync(user, "Librarian");
            return RedirectToAction("AllUsers");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(role);
            return RedirectToAction("AllUsers");
        }
        [HttpPost]
        public async Task<IActionResult> CreateAdminRole()
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            return RedirectToAction("AllUsers");
        }
        [HttpPost]
        public async Task<IActionResult> CreateLibrarianRole()
        {
            await _roleManager.CreateAsync(new IdentityRole("Librarian"));
            return RedirectToAction("AllUsers");
        }
    }
}
