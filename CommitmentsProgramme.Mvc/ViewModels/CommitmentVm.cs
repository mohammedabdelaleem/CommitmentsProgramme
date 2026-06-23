using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommitmentsProgramme.Mvc.ViewModels;

public class CommitmentVm
{
  public string Title { get; set; } = string.Empty;

  public TimeOnly? Time { get; set; }

  public Guid CommitmentTypeId { get; set; }

  public Guid PriorityId { get; set; }

  public List<Guid> BranchIds { get; set; } = new();

  public string? Location { get; set; }

  public string? Attendance { get; set; }

  public string? Notes { get; set; }
}