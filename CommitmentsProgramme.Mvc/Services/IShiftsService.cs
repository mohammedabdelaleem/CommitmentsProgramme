

using CommitmentsProgramme.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace CommitmentsProgramme.Mvc.Services;

public interface IShiftsService
{

       Task DeleteShifts(int id);
           Task EditShifts(Shiftvm vm ,string name) ;
     Task<List<Shiftvm>> GetAllShift(DateOnly? date) ;
       Task AddShift(ShiftAddVm shift ,string name );
Task<List<SelectListItem>> GetRanksShift();
       Task<Shifts?> GetByIdAsync(int id);


}