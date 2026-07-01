using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommitmentsProgramme.Mvc.ViewModels;

public class DailyPlanVm
{
  public int Id { get; set; }

  public DateOnly PlanDate { get; set; }

  public int SeniorOfficerId { get; set; }

  public int DutyOfficerId { get; set; }

  public List<CommitmentVm> Commitments { get; set; } =[];


    // SELECT LISTS
    public List<SelectListItem> Priorities { get; set; } =[];

    public List<SelectListItem> CommitmentTypes { get; set; } =[];

    public List<SelectListItem> Branches { get; set; } =[];

    public List<SelectListItem> Places { get; set; } =[];

    public List<SelectListItem> Attendances { get; set; } =[];

    public List<SelectListItem> SeniorOfficers { get; set; } =[];

    public List<SelectListItem> DutyOfficers { get; set; } =[];
}