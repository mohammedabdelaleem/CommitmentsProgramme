
using CommitmentsProgramme.Utilities.Abstractions.RegEx;

namespace CommitmentsProgramme.Mvc.ViewModels;

public class LoginRequestVm
{
  [Required(ErrorMessage = "{0} مطلوب")]
  [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
  [Display(Name = "البريد الإلكتروني")]
  public string Email { get; set; } = string.Empty;

  [Required(ErrorMessage = "{0} مطلوب")]
  [RegularExpression(
      RegexPatterns.Password,
      ErrorMessage = "كلمة المرور غير صحيحة")]
  [DataType(DataType.Password)]
  [Display(Name = "كلمة المرور")]
  public string Password { get; set; } = string.Empty;

  [Display(Name = "تذكرني")]
  public bool RememberMe { get; set; }

}

