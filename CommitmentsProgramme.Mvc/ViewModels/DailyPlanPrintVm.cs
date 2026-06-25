namespace CommitmentsProgramme.Mvc.ViewModels
{
    public class DailyPlanPrintVm
    {
        public DateOnly PlanDate { get; set; }

        public string SeniorOfficerName { get; set; }
        public string SeniorOfficerRank { get; set; }
        public string SeniorOfficerPhone { get; set; }

        public string DutyOfficerName { get; set; }
        public string DutyOfficerRank { get; set; }
        public string DutyOfficerPhone { get; set; }

        public List<CommitmentPrintVm> OutsideCommitments { get; set; } = [];
        public List<CommitmentPrintVm> InsideCommitments { get; set; } = [];
    }
}
