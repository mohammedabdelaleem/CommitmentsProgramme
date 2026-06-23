

//using System.Security.Claims;

using Humanizer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Stripe;
using System.Security.Claims;
using System.Security.Principal;
using CommitmentsProgramme.Utilities.Extensions;

namespace CommitmentsProgramme.Mvc.Controllers;

public class AuthController
    (UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    RoleManager<ApplicationRole> roleManager,
    IEmailService emailService,
    AuthService authService) : BaseController(userManager)
{
  private readonly UserManager<ApplicationUser> _userManager = userManager;
  private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
  private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
  private readonly IEmailService _emailService = emailService;
  private readonly AuthService _authService = authService;

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


      // confirm email
      return await ConfirmEmail(user);
    }

    foreach (var error in result.Errors)
    {
      ModelState.AddModelError("", error.Description);
    }

    model.Roles = await _roleManager.Roles.Where(x => !x.IsDeleted).ToListAsync(cancellationToken);
    return View(model);
  }

  private async Task<IActionResult> ConfirmEmail(ApplicationUser user)
  {
    // Generate email confirmation token
    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

    var confirmationLink = Url.Action(
        nameof(ConfirmEmail),
        "Auth",
        new { userId = user.Id, token },
        protocol: Request.Scheme);

    var subject = "Confirm your email - Wegoo";
    var body = $"Hi {user.FullName},<br/>" +
               $"Please confirm your account by clicking this link: <a href='{confirmationLink}'>Confirm Email</a>";

    await _emailService.SendEmailAsync(user.Email!, subject, body);

    // Show a view that tells the user to check their email
    return View("RegisterConfirmation");
  }

  public async Task<IActionResult> ConfirmEmail(string userId, string token)
  {
    if (userId == null || token == null)
      return RedirectToAction("Index", "Home");

    var user = await _userManager.FindByIdAsync(userId);
    if (user == null)
      return NotFound("User not found");

    var result = await _userManager.ConfirmEmailAsync(user, token);
    if (result.Succeeded)
    {
      TempData["success"] = "Email Confirmed Successfuly";
      return View("ConfirmEmailSuccess");
    }

    TempData["success"] = "Error While Confirming Email";
    return View("ConfirmEmailFailed");
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
      // adding imporatant user claims instead of loading the whole user
      //        await _userManager.AddClaimAsync(user,
      //            new Claim(SharedData.CustomeClaims.FullName, user.FullName));

      //        await _userManager.AddClaimAsync(user,
      //new Claim(SharedData.CustomeClaims.ImageName, user.ImageName));

      if (await _userManager.IsInRoleAsync(user, DefaultRoles.Admin))
        return RedirectToAction("Index", "Home", new { area = DefaultRoles.Admin });

      return string.IsNullOrEmpty(returnUrl) ? Redirect("~/") : Redirect(returnUrl);
    }

    if (loginResult.IsLockedOut)
    {
      ModelState.AddModelError(string.Empty, "Your account has been locked. Please try again later.");
      return View(request);
    }

    if (loginResult.IsNotAllowed)
    {
      //ModelState.AddModelError(string.Empty, "You are not allowed to login. Please confirm your email or contact support.");
      //return View(request);

      return await ConfirmEmail(user);

    }

    if (loginResult.RequiresTwoFactor)
    {
      // redirect to 2FA page if you support 2FA
      return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = true });
    }

    // Default: invalid login
    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
    return View(request);
  }


  public async Task<IActionResult> Logout()
  {
    await _signInManager.SignOutAsync();
    return RedirectToAction(nameof(Login));
  }

  //todo: linking emails for the same user 
  //todo:facebook when i return into home ; i need my phone where all accounts found 
  public async Task<IActionResult> ExternalLogin(string provider)
  {
    var redirectUrl = Url.Action(action: nameof(ExternalLoginCallback), controller: "Auth");
    var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

    return Challenge(properties, provider);
  }

  public async Task<IActionResult> ExternalLoginCallback()
  {
    var info = await _signInManager.GetExternalLoginInfoAsync();

    if (info == null)
      return RedirectToAction(nameof(Login));

    var provider = info.LoginProvider;
    var providerKey = info.ProviderKey;


    var user = await _userManager.FindByLoginAsync(provider, providerKey);

    if (user == null)
    {
      // Try get email : problem with github ==> null
      var email = info.Principal.FindFirstValue(ClaimTypes.Email);

      // fallback if null
      if (string.IsNullOrEmpty(email))
      {
        email = $"{providerKey}@{provider}.local";
        //I don’t have a real email, so I’ll generate a safe internal one
        //your REAL identity is this: (provider, providerKey) NOT email. 
        //Email is just: UI field ,optional metadata
        //Later: ask user to add real email(optional) ;Instead of forcing it during OAuth - GitHub ≠ guaranteed email provider
      }


      user = new ApplicationUser
      {
        Email = email,
        UserName = email,
        FullName = info.Principal.FindFirstValue(ClaimTypes.Name),
        EmailConfirmed = true,
        CreatedAt = DateTime.UtcNow,
      };

      var result = await _userManager.CreateAsync(user);

      if (!result.Succeeded)
        return RedirectToAction(nameof(Login));


      await _userManager.AddLoginAsync(user, info); // insert into AspNetUserLogins


      await _userManager.AddToRoleAsync(user, DefaultRoles.Member);

      // adding imporatant user claims instead of loading the whole user
      //await _userManager.AddClaimAsync(user,
      //             new Claim(SharedData.CustomeClaims.FullName, user.FullName));
      //await _userManager.AddClaimAsync(user, new Claim(SharedData.CustomeClaims.ImageName, user.ImageName));
    }


    await _signInManager.SignInAsync(user, isPersistent: false);

    return RedirectToAction("Index", "Home");
  }

  public IActionResult AccessDenied()
  {
    return View();
  }


  public IActionResult VerifyEmail()
  {
    return View();
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> VerifyEmail(VerifyEmailVm model)
  {
    if (!ModelState.IsValid)
      return View(model);

    if (await _userManager.FindByEmailAsync(model.Email) is not { } user)
    {
      ModelState.AddModelError("", "User Not Found");
      return View(model);
    }

    var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
    var resetLink = Url.Action("ChangePassword", "Auth", new { email = user.Email, token = resetToken }, Request.Scheme);

    var subject = "Reset Password";
    var body = $"Please Reset Your Password By Clicking Here: <a href='{resetLink}'>Reset Password</a>";

    await _emailService.SendEmailAsync(user.Email, subject, body);

    return RedirectToAction("SentEmail");
  }


  public IActionResult SentEmail()
  {
    return View();
  }

  public IActionResult ChangePassword(string email, string token)
  {
    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
      return RedirectToAction(nameof(VerifyEmail));


    return View(new ChangePasswordVm { Email = email, Token = token, NewPassword = "xxx", ConfirmedPassword = "xxx" });
  }


  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> ChangePassword(ChangePasswordVm model)
  {
    //if (!ModelState.IsValid)
    //	{
    //		ModelState.AddModelError("", "Something Went Wrong");
    //		return View(model); 
    //	}

    if (await _userManager.FindByEmailAsync(model.Email) is not { } user)
    {
      ModelState.AddModelError("", "User Not Found");
      return View(model);
    }


    var resetResult = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
    if (!resetResult.Succeeded)
    {
      foreach (var error in resetResult.Errors)
        ModelState.AddModelError("", error.Description);

      return View(model);
    }


    return RedirectToAction("Login");

  }

}
