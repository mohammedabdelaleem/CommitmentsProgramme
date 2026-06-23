namespace CommitmentsProgramme.Domain.Entities;

public class Commitment : BaseEntity
{
  [Required(ErrorMessage = "{0} مطلوب")]
  [Display(Name = "اسم الالتزام")]
  [StringLength(500)]
  public string Title { get; set; } = string.Empty;

  [Display(Name = "التوقيت")]
  public TimeOnly? Time { get; set; }

 
  [Display(Name = "ملاحظات")]
  public string? Notes { get; set; }


  [Display(Name = "نوع الالتزام")]
  public int CommitmentTypeId { get; set; }

  public CommitmentType CommitmentType { get; set; } = default!;

  [Display(Name = "الأولوية")]
  public int PriorityId { get; set; }

  public Priority Priority { get; set; } = default!;

  public int DailyPlanId { get; set; }

  public DailyPlan DailyPlan { get; set; } = default!;

    public List<Attendance> Attendances { get; set; } = [];
    public List<Branch> Branches { get; set; } = [];

    public int PlaceId { get; set; }
    public Place Place { get; set; } 
}