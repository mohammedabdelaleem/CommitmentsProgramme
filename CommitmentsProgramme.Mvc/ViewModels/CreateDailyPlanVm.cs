namespace CommitmentsProgramme.Mvc.ViewModels;

public class CreateDailyPlanVm
{
  [Display(Name = "التاريخ")]
  public DateOnly PlanDate { get; set; }

  [Display(Name = "الضابط العظيم")]
  public int SeniorOfficerId { get; set; }

  [Display(Name = "الضابط النوبتجي")]
  public int DutyOfficerId { get; set; }

  public List<CreateCommitmentVm> Commitments { get; set; } = [];
}