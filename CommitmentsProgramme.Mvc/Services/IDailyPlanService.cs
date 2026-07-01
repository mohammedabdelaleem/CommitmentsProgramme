namespace CommitmentsProgramme.Mvc.Services;

public interface IDailyPlanService
{
    Task<DailyPlanDetailsVm> GetDetailsAsync(DailyPlanRequestVM requestVM, CancellationToken cancellationToken = default);
    Task<DailyPlanVm> GetForEditAsync(int id, CancellationToken cancellationToken = default);

    Task SaveAsync(DailyPlanVm vm, string userFullName, CancellationToken cancellationToken = default);

    Task<DailyPlanPrintVm> GetForPrintAsync(int id, CancellationToken cancellationToken = default);
}