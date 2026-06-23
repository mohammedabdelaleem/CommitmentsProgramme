
using Microsoft.AspNetCore.Authorization;
using CommitmentsProgramme.Utilities.Extensions;

namespace CommitmentsProgramme.Mvc.Areas.Admin.Controllers;

[Area(DefaultRoles.Admin)]
[Authorize(Roles = DefaultRoles.Admin)]

public class UserController(
	UserManager<ApplicationUser> userManager,
	SignInManager<ApplicationUser> signInManager,
	RoleManager<ApplicationRole> roleManager) : Controller
{
	private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

	public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
	{
		var model = new List<UserRolesVm>();

		// all users without { me [login user] }
		var users = await _userManager.Users.Where(x => x.Id != User.GetUserId()).ToListAsync(cancellationToken);

		foreach (var user in users)
		{
			var userRoles = await _userManager.GetRolesAsync(user);

			model.Add(new UserRolesVm { User = user, UserRoles = (List<string>)userRoles });
		}

		// all roles
		ViewBag.AllRoles = await _roleManager.Roles.ToListAsync(cancellationToken);


		return View(model);
	}


	//[HttpPost]
	public async Task<IActionResult> UpdateUserRole(UserRoleRequestVm request, CancellationToken cancellationToken = default)
	{
		var user = await _userManager.FindByIdAsync(request.UserId);

		var result = await _userManager.AddToRoleAsync(user!, request.RoleName);


		// success : user doen't have this role and added To role Successfully
		// !Success : user is already have a role and now \|/
		// else : removing user from the existing role

		if (!result.Succeeded)
		{
			await _userManager.RemoveFromRoleAsync(user! , request.RoleName);
			TempData["success"] = "User Removed From Role successfully";
		}

		TempData["success"] = "User Added To Role successfully";

		return RedirectToAction(nameof(Index));

	}


	public async Task<IActionResult> LockUnlock(string id)
	{
		if (string.IsNullOrEmpty(id))
			return View("NotFound");

		var user = await _userManager.FindByIdAsync(id);
		if (user == null)
			return View("NotFound");

		if (user.LockoutEnd != null && user.LockoutEnd > DateTime.UtcNow)
		{
			//  Unlock
			user.LockoutEnd = DateTime.UtcNow;
			TempData["success"] = "User unlocked successfully";
		}
		else
		{
			//  Lock for  
			user.LockoutEnd = DateTime.UtcNow.AddDays(180);
			TempData["success"] = "User locked successfully";
		}

		await _userManager.UpdateAsync(user);
        await _signInManager.RefreshSignInAsync(user);

        return RedirectToAction(nameof(Index));
	}


	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken = default)
	{
		var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
		if (user == null)
			return Json(new { success = false, message = "User not found." });

		var result = await _userManager.DeleteAsync(user);

		if (result.Succeeded)
			return Json(new { success = true, message = "User deleted successfully." });

		return Json(new { success = false, message = "Error while deleting the User." });
	}

}



