namespace CommitmentsProgramme.Mvc.ViewModels
{
    public class DailyPlanReportVm
    {
        public DateOnly PlanDate { get; set; }

        public string SeniorOfficerRank { get; set; } = string.Empty;
        public string SeniorOfficerName { get; set; } = string.Empty;
        public string SeniorOfficerPhone { get; set; } = string.Empty;

        public string DutyOfficerRank { get; set; } = string.Empty;
        public string DutyOfficerName { get; set; } = string.Empty;
        public string DutyOfficerPhone { get; set; } = string.Empty;

        public List<CommitmentReportVm> InternalCommitments { get; set; } = [];
        public List<CommitmentReportVm> ExternalCommitments { get; set; } = [];
    }
}
