using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using CommitmentsProgramme.Utilities.Extensions;

namespace CommitmentsProgramme.Mvc.Controllers;
public class BaseController(UserManager<ApplicationUser> userManager) : Controller
{
	protected readonly UserManager<ApplicationUser> _userManager = userManager;

	public override async Task OnActionExecutionAsync(
		ActionExecutingContext context, 
		ActionExecutionDelegate next)
	{
		ApplicationUser user = null;
		if (User.Identity.IsAuthenticated)
		{
			var userId = User.GetUserId();
			 user = await _userManager.FindByIdAsync(userId);

			HttpContext.Items["CurrentUserId"] = user.Id;
			HttpContext.Items["CurrentUser"] = user;

		}
		else
		{
			 user = await _userManager.FindByIdAsync(DefaultUsers.DummyUserId);

			HttpContext.Items["CurrentUserId"] = user.Id;
			HttpContext.Items["CurrentUser"] = user;

		}
		await next();
	}

    public ApplicationUser? CurrentUser =>
    HttpContext?.Items["CurrentUser"] as ApplicationUser;

	public string? CurrentUserId =>
		HttpContext?.Items["CurrentUserId"] as string;
}
