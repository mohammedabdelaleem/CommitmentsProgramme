namespace CommitmentsProgramme.Domain.Entities;

public class Commitment : BaseEntity
{
  [Required(ErrorMessage = "{0} مطلوب")]
  [Display(Name = "اسم الالتزام")]
  [StringLength(500)]
  public string Title { get; set; } = string.Empty;

  [Display(Name = "الوصف")]
  public string? Description { get; set; }

  [Required(ErrorMessage = "{0} مطلوب")]
  [Display(Name = "نوع الالتزام")]
  public Guid CommitmentTypeId { get; set; }

  public CommitmentType CommitmentType { get; set; } = default!;

  [Required(ErrorMessage = "{0} مطلوب")]
  [Display(Name = "الأولوية")]
  public Guid PriorityId { get; set; }

  public Priority Priority { get; set; } = default!;

  [Required(ErrorMessage = "{0} مطلوب")]
  [Display(Name = "الفرع المختص")]
  public Guid BranchId { get; set; }

  public Branch Branch { get; set; } = default!;

  [Display(Name = "التاريخ")]
  public DateOnly CommitmentDate { get; set; }

  [Display(Name = "الوقت")]
  public TimeOnly CommitmentTime { get; set; }

  [Display(Name = "المكان")]
  [StringLength(300)]
  public string? Location { get; set; }

  [Display(Name = "الحضور")]
  public string? Attendance { get; set; }

  [Display(Name = "ملاحظات")]
  public string? Notes { get; set; }
}