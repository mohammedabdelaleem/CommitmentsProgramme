namespace CommitmentsProgramme.Mvc.ViewModels
{
    public class DailyPlanListVm
    {
        public int Id { get; set; }
        public DateOnly PlanDate { get; set; }

        public string SeniorOfficerName { get; set; } = string.Empty;
        public string DutyOfficerName { get; set; } = string.Empty;

        public int CommitmentsCount { get; set; }
    }
}
