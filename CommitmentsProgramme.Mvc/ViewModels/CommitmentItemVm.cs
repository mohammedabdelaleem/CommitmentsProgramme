namespace CommitmentsProgramme.Mvc.ViewModels;

public class CommitmentItemVm
{
  public int? Id { get; set; }

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
  public int CommitmentTypeId { get; set; }

  [Display(Name = "الأولوية")]
  public int PriorityId { get; set; }

  [Display(Name = "الفروع المختصة")]
  public List<int> BranchIds { get; set; } = new();
}