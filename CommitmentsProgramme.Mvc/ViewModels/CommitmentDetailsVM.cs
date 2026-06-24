namespace CommitmentsProgramme.Mvc.ViewModels
{
    public class CommitmentDetailsVM
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public TimeOnly? Time { get; set; }

        public string CommitmentType { get; set; }

        public string Priority { get; set; }

        // Multi Select Branches
        public List<int> BranchIds { get; set; } = new();

        // Multi Select Attendances
        public List<int> AttendanceIds { get; set; } = new();

        // Single Select Place
        public string Place { get; set; }

        public string? Notes { get; set; }
    }
}
