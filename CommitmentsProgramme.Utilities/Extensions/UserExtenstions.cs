using System.Security.Claims;
using CommitmentsProgramme.Utilities.Abstractions.Consts;
using static CommitmentsProgramme.Utilities.Abstractions.Consts.SharedData;

namespace CommitmentsProgramme.Utilities.Extensions;

public static class UserExtenstions
{
	public static string? GetUserId(this ClaimsPrincipal user) =>
		user.FindFirstValue(ClaimTypes.NameIdentifier);

	public static string? GetUserFullname(this ClaimsPrincipal user) =>
		user.FindFirstValue(CustomeClaims.FullName);

	
}
