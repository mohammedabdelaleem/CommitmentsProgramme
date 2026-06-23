using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommitmentsProgramme.Domain.Entities;

public class DailyPlan : BaseEntity
{
  [Required(ErrorMessage = "{0} مطلوب")]
  [Display(Name = "التاريخ")]
  public DateOnly PlanDate { get; set; }

  [Required(ErrorMessage = "{0} مطلوب")]
  [Display(Name = "الضابط العظيم")]
  public int SeniorOfficerId { get; set; }

  public Officer SeniorOfficer { get; set; } = default!;

  [Required(ErrorMessage = "{0} مطلوب")]
  [Display(Name = "الضابط النوبتجي")]
  public int DutyOfficerId { get; set; }

  public Officer DutyOfficer { get; set; } = default!;

  public ICollection<Commitment> Commitments { get; set; }
      = new List<Commitment>();
}