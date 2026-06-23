
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommitmentsProgramme.Domain.Entities;

public class Officer : BaseEntity
{
  [Required(ErrorMessage = "{0} مطلوب")]
  [Display(Name = "الاسم")]
  public string FullName { get; set; } = string.Empty;

  [Required(ErrorMessage = "{0} مطلوب")]
  [Display(Name = "الرتبة")]
  public int RankId { get; set; }

  public Rank Rank { get; set; } = default!;

  public bool IsActive { get; set; } = true;
}