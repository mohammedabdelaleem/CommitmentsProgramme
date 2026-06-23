using CommitmentsProgramme.Utilities.Abstractions.RegEx;

namespace CommitmentsProgramme.Mvc.ViewModels;

public class RegisterRequestVm
{
  [Required(ErrorMessage = "{0} مطلوب")]
  [StringLength(225, MinimumLength = 8)]
  [Display(Name = "الاسم")]
  public string FullName { get; set; } = string.Empty;

  [Required(ErrorMessage = "{0} مطلوب")]
  [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
  [Display(Name = "البريد الإلكتروني")]
  public string Email { get; set; } = string.Empty;

  [Required(ErrorMessage = "{0} مطلوب")]
  [RegularExpression(
      RegexPatterns.Password,
      ErrorMessage = "كلمة المرور لا تحقق الشروط المطلوبة")]
  [Display(Name = "كلمة المرور")]
  public string Password { get; set; } = string.Empty;

  [Required(ErrorMessage = "{0} مطلوب")]
  [Compare(nameof(Password),
      ErrorMessage = "كلمتا المرور غير متطابقتين")]
  [Display(Name = "تأكيد كلمة المرور")]
  public string ConfirmPassword { get; set; } = string.Empty;

  [Required(ErrorMessage = "{0} مطلوب")]
  [RegularExpression(RegexPatterns.Phone,
      ErrorMessage = "رقم المحمول غير صحيح")]
  [Display(Name = "رقم المحمول")]
  public string Phone { get; set; } = string.Empty;

  [Display(Name = "الصلاحية")]
  public string? Role { get; set; }

  [Display(Name = "السيرة الذاتية")]
  public string? Bio { get; set; }

}

