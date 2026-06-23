using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommitmentsProgramme.Mvc.ViewModels;

public class CommitmentVm
{
    public string Title { get; set; } = string.Empty;

    public TimeOnly? Time { get; set; }

    public int CommitmentTypeId { get; set; }

    public int PriorityId { get; set; }

    // Multi Select Branches
    public List<int> BranchIds { get; set; } = new();

    // Multi Select Attendances
    public List<int> AttendanceIds { get; set; } = new();

    // Single Select Place
    public int PlaceId { get; set; }

    public string? Notes { get; set; }
}