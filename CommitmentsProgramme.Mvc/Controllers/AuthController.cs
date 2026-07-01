using Microsoft.AspNetCore.Authorization;

namespace CommitmentsProgramme.Mvc.Controllers;

public class AuthController
    (UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    RoleManager<ApplicationRole> roleManager
   ) : BaseController(userManager)
{
  private readonly UserManager<ApplicationUser> _userManager = userManager;
  private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
  private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

  public async Task<IActionResult> Register(string returnUrl = null)
  {
    ViewBag.ReturnUrl = returnUrl;
    var registerRequestVm = new RegisterVm
    {
      Roles = await _roleManager.Roles.Where(x => !x.IsDeleted).ToListAsync()
    };

    return View(registerRequestVm);
  }


  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Register(RegisterVm model, IFormFile? file, string returnUrl = null, CancellationToken cancellationToken = default)
  {
    if (!ModelState.IsValid)
    {
      model.Roles = await _roleManager.Roles.Where(x => !x.IsDeleted).ToListAsync(cancellationToken);
      return View(model);
    }

    if (await _userManager.Users.AnyAsync(x => x.Email == model.RegisterReq.Email, cancellationToken))
    {
      ModelState.AddModelError("Email.Duplicate", "This Email Is Found");

      model.Roles = await _roleManager.Roles.Where(x => !x.IsDeleted).ToListAsync(cancellationToken);
      return View(model);
    }

    // create new user
    var user = new ApplicationUser
    {
      FullName = model.RegisterReq.FullName,
      Email = model.RegisterReq.Email,
      UserName = model.RegisterReq.Email,
      PhoneNumber = model.RegisterReq.Phone,
      Bio = model.RegisterReq.Bio,
      CreatedAt = DateTime.UtcNow,
    };

    var result = await _userManager.CreateAsync(user, model.RegisterReq.Password);

    if (result.Succeeded)
    {

      // Assign role
      var selectedRole = !string.IsNullOrWhiteSpace(model.RegisterReq.Role)
          ? model.RegisterReq.Role
          : DefaultRoles.Member;

      if (!await _roleManager.RoleExistsAsync(selectedRole))
        await _roleManager.CreateAsync(new ApplicationRole { Name = selectedRole });

      await _userManager.AddToRoleAsync(user, selectedRole);

            // Sign the user in immediately
            await _signInManager.SignInAsync(user, isPersistent: false);

            if (await _userManager.IsInRoleAsync(user, DefaultRoles.Admin))
            {
                return RedirectToAction("Index", "Home", new { area = DefaultRoles.Admin });
            }

            return string.IsNullOrEmpty(returnUrl)
                ? Redirect("~/")
                : Redirect(returnUrl);
        }

    foreach (var error in result.Errors)
    {
      ModelState.AddModelError("", error.Description);
    }

    model.Roles = await _roleManager.Roles.Where(x => !x.IsDeleted).ToListAsync(cancellationToken);
    return View(model);
  }


  public IActionResult Login(string returnUrl = null)
  {
    ViewBag.ReturnUrl = returnUrl;
    return View(new LoginRequestVm());
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Login(LoginRequestVm request, string returnUrl = null, CancellationToken cancellationToken = default)
  {
    if (!ModelState.IsValid)
      return View(request);

    var user = await _userManager.FindByEmailAsync(request.Email);
    if (user == null)
    {
      // don't tell the user why failed login attempt , may be he is a hacker
      ModelState.AddModelError(string.Empty, "Invalid login attempt.");
      return View(request);
    }

    // Ensure lockout is enabled for this user
    // If it’s true, the user account can be locked out (temporarily disabled) after multiple failed login attempts.
    // If it’s false, no matter how many times the user enters the wrong password, the lockout mechanism will not be applied.
    if (!user.LockoutEnabled)
    {
      await _userManager.SetLockoutEnabledAsync(user, true);
    }

    // adding fullname to user claims ==> logged in user has full name at his claims 
    //await _authService.UpdateUserClaimsAsync(user);	no need for this claims are generated directly from: user.FullName


    var loginResult = await _signInManager.PasswordSignInAsync(
    user.UserName!,
    request.Password,
    isPersistent: request.RememberMe,   // Keep the user logged in if and only if he want this
    lockoutOnFailure: true // Count failed attempts toward lockout
);

        // PasswordSignInAsync() already creates the authentication cookie.
        // calls Identity internally ===> Identity asks: ApplicationClaimsPrincipalFactory to build the ClaimsPrincipal
        // Your custom claims are automatically included.

        if (loginResult.Succeeded)
        {
            if (await _userManager.IsInRoleAsync(user, DefaultRoles.Admin))
                return RedirectToAction("Index", "Home", new { area = DefaultRoles.Admin });

            return string.IsNullOrEmpty(returnUrl)
                ? Redirect("~/")
                : Redirect(returnUrl);
        }

        if (loginResult.IsLockedOut)
        {
            ModelState.AddModelError("", "Your account has been locked.");
            return View(request);
        }

        if (loginResult.RequiresTwoFactor)
        {
            return RedirectToAction("SendCode",
                new { ReturnUrl = returnUrl, RememberMe = true });
        }

        ModelState.AddModelError("", "Invalid email or password.");
        return View(request);
    }


  public async Task<IActionResult> Logout()
  {
    await _signInManager.SignOutAsync();
    return RedirectToAction(nameof(Login));
  }


  public IActionResult AccessDenied()
  {
    return View();
  }


    [Authorize]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordVm model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.GetUserAsync(User);

        if (user == null)
            return RedirectToAction(nameof(Login));

        var result = await _userManager.ChangePasswordAsync(
            user,
            model.CurrentPassword,
            model.NewPassword);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        // Refresh authentication cookie
        await _signInManager.RefreshSignInAsync(user);

        TempData["success"] = "Password changed successfully.";

        return RedirectToAction("Index", "Home");
    }
}
