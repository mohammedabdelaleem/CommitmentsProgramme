

namespace CommitmentsProgramme.Mvc.ViewModels;

public class RoleVm
{
  [ValidateNever]
  public string? Id { get; set; }

  [Required(ErrorMessage = "{0} مطلوب")]
  [MinLength(3,
      ErrorMessage = "{0} يجب أن يحتوي على 3 أحرف على الأقل")]
  [Display(Name = "اسم الصلاحية")]
  public string Name { get; set; } = string.Empty;

  [Display(Name = "الصلاحية الافتراضية")]
  public bool IsDefault { get; set; }
}
