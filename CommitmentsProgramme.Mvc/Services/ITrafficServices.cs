

using CommitmentsProgramme.Mvc.ViewModels;
namespace CommitmentsProgramme.Mvc.Services;

public interface ITrafficServices
{

       Task DeleteTraffic(int id);
           Task EditTraffic(TrafficPlanEditVm vm ,string name) ;
 Task<List<TrafficPlanVm>> GetAllTraffic(DateOnly? date) ;
     Task AddTraffic(TrafficPlanAddVm plane ,string name );

Task<TrafficPlanCreateVm> GetCreateData();
}