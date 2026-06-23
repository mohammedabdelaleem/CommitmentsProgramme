using System.Runtime.InteropServices;

namespace CommitmentsProgramme.Mvc.ViewModels;

public class UserRoleRequestVm
{
  [Required(ErrorMessage = "المستخدم مطلوب")]
  [Display(Name = "المستخدم")]
  public string UserId { get; set; } = string.Empty;

  [Required(ErrorMessage = "الصلاحية مطلوبة")]
  [Display(Name = "الصلاحية")]
  public string RoleName { get; set; } = string.Empty;
}

