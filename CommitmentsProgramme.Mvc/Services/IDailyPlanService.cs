namespace CommitmentsProgramme.Mvc.Services;

public interface IDailyPlanService
{

    Task<DailyPlanVm> GetForEditAsync(int id);

    Task SaveAsync(DailyPlanVm vm, string userFullName);
}