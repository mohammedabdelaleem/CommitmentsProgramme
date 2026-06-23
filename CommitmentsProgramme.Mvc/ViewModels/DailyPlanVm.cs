using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommitmentsProgramme.Mvc.ViewModels;

public class DailyPlanVm
{
  public int? Id { get; set; }

  public DateOnly PlanDate { get; set; }

  public int SeniorOfficerId { get; set; }

  public int DutyOfficerId { get; set; }

  public List<CommitmentVm> Commitments { get; set; } = new();


    // SELECT LISTS
    public List<SelectListItem> Priorities { get; set; } = new();

    public List<SelectListItem> CommitmentTypes { get; set; } = new();

    public List<SelectListItem> Branches { get; set; } = new();

    public List<SelectListItem> Places { get; set; } = new();

    public List<SelectListItem> Attendances { get; set; } = new();

    public List<SelectListItem> SeniorOfficers { get; set; } = new();

    public List<SelectListItem> DutyOfficers { get; set; } = new();
}