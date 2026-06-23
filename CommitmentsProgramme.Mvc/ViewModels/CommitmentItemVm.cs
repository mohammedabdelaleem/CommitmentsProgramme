namespace CommitmentsProgramme.Mvc.ViewModels;

public class CommitmentItemVm
{
  public Guid? Id { get; set; }

  [Display(Name = "الالتزام")]
  [Required(ErrorMessage = "{0} مطلوب")]
  public string Title { get; set; } = string.Empty;

  [Display(Name = "التوقيت")]
  public TimeOnly? Time { get; set; }

  [Display(Name = "المكان")]
  public string? Location { get; set; }

  [Display(Name = "الحضور")]
  public string? Attendance { get; set; }

  [Display(Name = "ملاحظات")]
  public string? Notes { get; set; }

  [Display(Name = "نوع الالتزام")]
  public Guid CommitmentTypeId { get; set; }

  [Display(Name = "الأولوية")]
  public Guid PriorityId { get; set; }

  [Display(Name = "الفروع المختصة")]
  public List<Guid> BranchIds { get; set; } = new();
}