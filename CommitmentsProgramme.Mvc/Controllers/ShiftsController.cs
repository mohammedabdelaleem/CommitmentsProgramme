using CommitmentsProgramme.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
namespace CommitmentsProgramme.Mvc.Controllers
{
    public class ShiftsController(
        IShiftsService shiftsService,
        IUnitOfWork unitOfWork) : Controller
    {
        private readonly  IShiftsService _shiftsService = shiftsService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

   public async Task<IActionResult> Index(DateOnly? date)
    {
        var model = await _shiftsService.GetAllShift(date);
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Ranks = await _shiftsService.GetRanksShift();
        return View(new ShiftAddVm());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]ShiftAddVm model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Ranks = await _shiftsService.GetRanksShift();
            return View(model);
        }

        await _shiftsService.AddShift(model, User.Identity?.Name ?? "System");

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var shift = await _shiftsService.GetByIdAsync(id);

        if (shift == null)
            return NotFound();

        var vm = new Shiftvm
        {
            Id = shift.Id,
            Name = shift.Name,
            Phone = shift.Phone,
            EnumSelect = (int)shift.shiftRank
        };

        ViewBag.Ranks = await _shiftsService.GetRanksShift();

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromBody]Shiftvm model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Ranks = await _shiftsService.GetRanksShift();
            return View(model);
        }

        await _shiftsService.EditShifts(model, User.Identity?.Name ?? "System");

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var shift = await _shiftsService.GetByIdAsync(id);

        if (shift == null)
            return NotFound();

        return View(shift);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _shiftsService.DeleteShifts(id);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var shift = await _shiftsService.GetByIdAsync(id);

        if (shift == null)
            return NotFound();

        return View(shift);
    }


    [HttpGet]
public async Task<IActionResult> GetRanks()
{
    var data = await _shiftsService.GetRanksShift();
    return Json(data);
}
}

}