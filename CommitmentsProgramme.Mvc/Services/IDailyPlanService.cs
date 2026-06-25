namespace CommitmentsProgramme.Mvc.Services;

public interface IDailyPlanService
{
    Task<DailyPlanDetailsVm> GetDetailsAsync(int id);
    Task<DailyPlanVm> GetForEditAsync(int id);

    Task SaveAsync(DailyPlanVm vm, string userFullName);

    Task<DailyPlanPrintVm> GetForPrintAsync(int id);
}