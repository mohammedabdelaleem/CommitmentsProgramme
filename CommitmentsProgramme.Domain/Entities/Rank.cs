namespace CommitmentsProgramme.Domain.Entities;

public class Rank : BaseEntity
{
  [Required(ErrorMessage = "{0} مطلوب")]
  [Display(Name = "اسم الرتبة")]
  [StringLength(100)]
  public string Name { get; set; } = string.Empty;

  [Display(Name = "الترتيب")]
  public int DisplayOrder { get; set; }

  [Display(Name = "نشط")]
  public bool IsActive { get; set; } = true;
}