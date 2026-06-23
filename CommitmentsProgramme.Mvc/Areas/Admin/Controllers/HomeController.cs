using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CommitmentsProgramme.Mvc.Areas.Admin.Controllers;

[Area(DefaultRoles.Admin)]
[Authorize(Roles = DefaultRoles.Admin)]
public class HomeController(
	IUnitOfWork unitOfWork,
	UserManager<ApplicationUser> userManager) : Controller
{

	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly UserManager<ApplicationUser> _userManager = userManager;

	public IActionResult Index()
	{
		return View();
	}

	public IActionResult Error()
	{
		return View();
	}


}


