namespace CommitmentsProgramme.Domain.Entities;

public class Branch : BaseEntity
{
  [Required(ErrorMessage = "{0} مطلوب")]
  [Display(Name = "اسم الفرع")]
  [StringLength(100, ErrorMessage = "{0} يجب ألا يزيد عن 100 حرف")]
  public string Name { get; set; } = string.Empty;


  //[Display(Name = "الترتيب")]
  //public int DisplayOrder { get; set; }

  
    [Display(Name = "نشط")]
  public bool IsActive { get; set; } = true;

    public ICollection<CommitmentBranch> CommitmentBranches { get; set; } = [];

}