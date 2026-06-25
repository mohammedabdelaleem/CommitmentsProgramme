namespace CommitmentsProgramme.Mvc.ViewModels
{
    public class CommitmentPrintVm
    {
        public string Title { get; set; }

        public string PlaceName { get; set; }

        public TimeOnly Time { get; set; }

        public string? Notes { get; set; }

        public List<string> Branches { get; set; } = [];

        public List<string> Attendances { get; set; } = [];
    }
}
