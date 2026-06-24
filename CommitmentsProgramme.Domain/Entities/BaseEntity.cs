namespace CommitmentsProgramme.Domain.Entities;

public abstract class BaseEntity
{
	public int Id { get; set; }

  [Display(Name = "انشاء بواسطة")]
  public string? CreatedBy { get; set; } = "admin";

  [Display(Name = "تعديل بواسطة")]
  public string? UpdatedBy { get; set; }

  [Display(Name = "وقت الانشاء ")]
  public DateTime CreatedDate { get; set; }

  [Display(Name = "وقت التعديل ")]
  public DateTime? UpdatedDate { get; set; }

}

