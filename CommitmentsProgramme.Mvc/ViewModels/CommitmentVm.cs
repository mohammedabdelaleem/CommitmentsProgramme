namespace CommitmentsProgramme.Mvc.ViewModels;

public class CommitmentVm
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public TimeOnly Time { get; set; }

    public int CommitmentTypeId { get; set; }

    public string CommitmentTypeName { get; set; } = "";

    public int PriorityId { get; set; }

    public string PriorityName { get; set; } = "";

    public int PlaceId { get; set; }

    public string PlaceName { get; set; } = "";

    public string? Notes { get; set; }

    public List<int> BranchIds { get; set; } = new();

    public List<int> AttendanceIds { get; set; } = new();

    // NEW
    public List<string> BranchNames { get; set; } = new();

    public List<string> AttendanceNames { get; set; } = new();
}