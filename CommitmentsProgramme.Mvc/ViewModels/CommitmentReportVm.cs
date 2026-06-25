namespace CommitmentsProgramme.Mvc.ViewModels
{
    public class CommitmentReportVm
    {
        public string Time { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string PlaceName { get; set; } = string.Empty;

        public List<string> BranchNames { get; set; } = [];

        public List<string> AttendanceNames { get; set; } = [];
    }
}
