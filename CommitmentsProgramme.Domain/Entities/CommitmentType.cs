using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommitmentsProgramme.Domain.Entities;

public class CommitmentType : BaseEntity
{
  [Required(ErrorMessage = "{0} مطلوب")]
  [Display(Name = "نوع الالتزام")]
  [StringLength(100)]
  public string Name { get; set; } = string.Empty;

  [Display(Name = "الترتيب")]
  public int DisplayOrder { get; set; }

  [Display(Name = "نشط")]
  public bool IsActive { get; set; } = true;
}
