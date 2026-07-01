namespace CommitmentsProgramme.Mvc.ViewModels
{
    public class DailyPlanDetailsVm
    {
        public int Id { get; set; }

        public DateOnly PlanDate { get; set; }

        public OfficerInfoVm SeniorOfficer { get; set; } = new();

        public OfficerInfoVm DutyOfficer { get; set; } = new();

        public List<CommitmentVm> OutsideCommitments { get; set; } = [];

        public List<CommitmentVm> InsideCommitments { get; set; } = [];
    }
}
