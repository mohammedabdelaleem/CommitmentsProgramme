using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using CommitmentsProgramme.Domain.Entities;
using CommitmentsProgramme.Infrastructure.Persistance;
using CommitmentsProgramme.Utilities.Extensions;
using CommitmentsProgramme.Mvc.ViewModels;

namespace CommitmentsProgramme.Mvc.Controllers;

[Authorize]
public class AccountController(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    AppDbContext appDbContext,
    AuthService authService) : BaseController(userManager)
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly AppDbContext _context = appDbContext;
	private readonly AuthService _authService = authService;

	public async Task<IActionResult> Index()
    {
        var userId = User.GetUserId()!;
        var user = (await _userManager.FindByIdAsync(userId));

        var updatedUserProfileVm = user.Adapt<UpdateProfileVM>();


        return View(updatedUserProfileVm);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeProfileImage(
     IFormFile image,
     CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var user = await _userManager.FindByIdAsync(userId);

        if (image == null || image.Length == 0)
            return BadRequest("No file uploaded");

        if (!image.ContentType.StartsWith("image/"))
            return BadRequest("Invalid file type");

       
        var result = await _userManager.UpdateAsync(user);
        await _signInManager.RefreshSignInAsync(user); // refresh cookies using my factory.

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new { success = true });
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateProfileInfo(
        UpdateProfileVM updateProfile,
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userId = User.GetUserId();
        var user = await _userManager.FindByIdAsync(userId);

    
        updateProfile.Adapt(user);

		//await _authService.UpdateUserClaimsAsync(user);

		var result = await _userManager.UpdateAsync(user);
        await _signInManager.RefreshSignInAsync(user); // This rebuilds the cookie using my factory.

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new
        {
            success = true,
            message = "Profile updated successfully"
        });
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeProfilePassword(
       ChangeProfilePasswordVM changeProfilePasswordVM,
       CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) // checks for not empty + [new = confirmed]
        {
            return BadRequest(ModelState);
        }

        var userId = User.GetUserId();
        var user = await _userManager.FindByIdAsync(userId);


        //bool isCurrentPassword = await _userManager.CheckPasswordAsync(user, changeProfilePasswordVM.CurrentPassword);

        var result = await _userManager.ChangePasswordAsync(user, changeProfilePasswordVM.CurrentPassword, changeProfilePasswordVM.NewPassword);

        if (result.Succeeded)
        {
            return Ok(new
            {
                success = true,
                message = "Password Updated Successfully"
            });
        }
        else
        {

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return BadRequest(ModelState);
        }
    }

}
