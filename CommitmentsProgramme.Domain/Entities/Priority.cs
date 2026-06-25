using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommitmentsProgramme.Domain.Entities;

public class Priority : BaseEntity
{
  [Required(ErrorMessage = "{0} مطلوب")]
  [Display(Name = "الأولوية")]
  [StringLength(50)]
  public string Name { get; set; } = string.Empty;

  [Display(Name = "لون العرض")]
  public string CssClass { get; set; } = "primary";

  //[Display(Name = "الترتيب")]
  //public int DisplayOrder { get; set; }

  [Display(Name = "نشط")]
  public bool IsActive { get; set; } = true;
}