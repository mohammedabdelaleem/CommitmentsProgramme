
namespace CommitmentsProgramme.Mvc.ViewModels;

public class CreateCommitmentVm
{
  [Required(ErrorMessage = "{0} مطلوب")]
  [Display(Name = "اسم الالتزام")]
  public string Title { get; set; } = string.Empty;

  [Display(Name = "الوصف")]
  public string? Description { get; set; }

  [Display(Name = "نوع الالتزام")]
  public CommitmentType Type { get; set; }

  [Display(Name = "الأولوية")]
  public Priority Priority { get; set; }

  [Display(Name = "التاريخ")]
  public DateOnly CommitmentDate { get; set; }

  [Display(Name = "الوقت")]
  public TimeOnly CommitmentTime { get; set; }

  [Display(Name = "المكان")]
  public string? Location { get; set; }

  [Display(Name = "الإدارة")]
  public string? Department { get; set; }

  public List<string> Attendances { get; set; } = [];
}