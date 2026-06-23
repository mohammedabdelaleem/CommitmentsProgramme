using System.Security.Claims;
using static CommitmentsProgramme.Utilities.Abstractions.Consts.SharedData;

namespace CommitmentsProgramme.Infrastructure.Services;
public class AuthService
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly SignInManager<ApplicationUser> _signInManager;

	public AuthService(
		UserManager<ApplicationUser> userManager,
		SignInManager<ApplicationUser> signInManager)
	{
		_userManager = userManager;
		_signInManager = signInManager;
	}

	public async Task UpdateUserClaimsAsyncX(ApplicationUser user) // Not Used Will Removed
	{
		var existingClaims = await _userManager.GetClaimsAsync(user);

		var fullNameClaim = existingClaims
			.FirstOrDefault(x => x.Type == CustomeClaims.FullName);

		if (fullNameClaim == null)
		{
			await _userManager.AddClaimAsync(
				user,
				new Claim(CustomeClaims.FullName, user.FullName)
			);
		}
		else if (fullNameClaim.Value != user.FullName)
		{
			await _userManager.ReplaceClaimAsync(
				user,
				fullNameClaim,
				new Claim(CustomeClaims.FullName, user.FullName)
			);
		}

		// VERY IMPORTANT
		await _signInManager.RefreshSignInAsync(user);
	}
}