namespace CommitmentsProgramme.Mvc.ViewModels;

public class ChangePasswordVm
{
    [Required(ErrorMessage = "{0} مطلوب")]
    [DataType(DataType.Password)]
    [Display(Name = "كلمة المرور الحالية")]
    public string CurrentPassword { get; set; }


    [Required(ErrorMessage = "{0} مطلوب")]
    [StringLength(40, MinimumLength = 7,
      ErrorMessage = "{0} يجب أن يكون بين {2} و {1} حرف")]
    [DataType(DataType.Password)]
    [Display(Name = "كلمة المرور الجديدة")]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "{0} مطلوب")]
    [Compare(nameof(NewPassword),
        ErrorMessage = "كلمتا المرور غير متطابقتين")]
    [DataType(DataType.Password)]
    [Display(Name = "تأكيد كلمة المرور")]
    public string ConfirmedPassword { get; set; } = string.Empty;



}