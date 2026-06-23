using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommitmentsProgramme.Mvc.ViewModels;

public class DailyPlanVm
{
  public Guid? Id { get; set; }

  public DateOnly PlanDate { get; set; }

  public Guid SeniorOfficerId { get; set; }

  public Guid DutyOfficerId { get; set; }

  public List<CommitmentVm> Commitments { get; set; } = new();

  // SELECT LISTS
  public List<SelectListItem> SeniorOfficers { get; set; } = new();
  public List<SelectListItem> DutyOfficers { get; set; } = new();

  public List<SelectListItem> Priorities { get; set; } = new();
  public List<SelectListItem> CommitmentTypes { get; set; } = new();
  public List<SelectListItem> Branches { get; set; } = new();
}