namespace CommitmentsProgramme.Mvc.ViewModels;

public class VerifyEmailVm
{

  [Required(ErrorMessage = "{0} مطلوب")]
  [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
  [Display(Name = "البريد الإلكتروني")] 
  public string Email { get; set; }
}


