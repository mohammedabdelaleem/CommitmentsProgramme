using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommitmentsProgramme.Mvc.ViewModels;

namespace CommitmentsProgramme.Mvc.Areas.Admin.Controllers;

[Area(DefaultRoles.Admin)]
[Authorize(Roles = DefaultRoles.Admin)]
public class RoleController(RoleManager<ApplicationRole> roleManager) : Controller
{
	private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

	public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
	{
		var roles = await _roleManager.Roles.ToListAsync(cancellationToken);
		return View(roles);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> SetDefault(string roleId, CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrEmpty(roleId))
		{
			TempData["error"] = "Invalid role selected.";
			return RedirectToAction(nameof(Index));
		}

		var roles = await _roleManager.Roles.ToListAsync(cancellationToken);
		var targetRole = roles.FirstOrDefault(r => r.Id == roleId);

		if (targetRole == null)
		{
			TempData["error"] = "Role not found.";
			return RedirectToAction(nameof(Index));
		}

		foreach (var role in roles)
		{
			bool shouldBeDefault = role.Id == roleId;
			if (role.IsDefault != shouldBeDefault)
			{
				role.IsDefault = shouldBeDefault;
				await _roleManager.UpdateAsync(role);
			}
		}

		TempData["success"] = $"'{targetRole.Name}' has been set as the default role!";
		return RedirectToAction(nameof(Index));
	}


	[HttpGet]
	public async Task<IActionResult> Edit(string? id, CancellationToken cancellationToken = default)
	{
		if (id == null)
			return View(new RoleVm());

		var role = await _roleManager.Roles
			.Where(x => x.Id == id)
			.ProjectToType<RoleVm>()
			.SingleOrDefaultAsync(cancellationToken);

		return View(role);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Save(RoleVm item, CancellationToken cancellationToken = default)
	{
		if (!ModelState.IsValid)
		{
			return View(nameof(Edit), item);
		}

		if (string.IsNullOrEmpty(item.Id))
		{

			if (await _roleManager.RoleExistsAsync(item.Name))
			{
				ModelState.AddModelError("", "Role already exists.");
				return View(nameof(Edit), item);
			}

			var newRole = new ApplicationRole
			{
				Name = item.Name,
				NormalizedName = item.Name.ToUpperInvariant(),
				IsDefault = item.IsDefault,
				CreatedAt = DateTime.UtcNow,
			};

			await _roleManager.CreateAsync(newRole);

			// Ensure only one default role
			if (item.IsDefault)
			{
				await UnsetOtherDefaultsAsync(newRole.Id, cancellationToken);
			}

			TempData["success"] = "Role has been created successfully!";
		}
		else
		{
			// UPDATE
			var targetRole = await _roleManager.Roles
				.FirstOrDefaultAsync(x => x.Id == item.Id, cancellationToken);

			if (targetRole == null)
			{
				TempData["error"] = "Role not found.";
				return RedirectToAction(nameof(Index));
			}

			targetRole.Name = item.Name;
			targetRole.NormalizedName = item.Name.ToUpperInvariant();


			await _roleManager.UpdateAsync(targetRole);

			if (item.IsDefault)
			{
				await UnsetOtherDefaultsAsync(targetRole.Id, cancellationToken);
			}

			TempData["success"] = "Role has been updated successfully!";
		}

		return RedirectToAction(nameof(Index));
	}

	private async Task UnsetOtherDefaultsAsync(string roleId, CancellationToken cancellationToken)
	{
		var roles = await _roleManager.Roles.ToListAsync(cancellationToken);

		foreach (var role in roles)
		{
			bool shouldBeDefault = role.Id == roleId;
			if (role.IsDefault != shouldBeDefault)
			{
				role.IsDefault = shouldBeDefault;
				await _roleManager.UpdateAsync(role);
			}
		}
	}


	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken = default)
	{
		var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
		if (role == null)
			return Json(new { success = false, message = Messages.ItemNotFound });

	


        try
        {

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
                return Json(new
                {
                    success = true,
                    message = Messages.SuccessRemoveItem
                });

			else
			{
                return Json(new
                {
                    success = false,
                    message = Messages.ErrorRemoveItem
                });
            }
                             }
        catch
        {
            return Json(new
            {
                success = false,
                message = Messages.ErrorRemoveItem
            });
        }
    }
}
