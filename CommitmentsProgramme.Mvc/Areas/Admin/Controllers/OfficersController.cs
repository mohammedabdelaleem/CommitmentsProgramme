using CommitmentsProgramme.Utilities.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommitmentsProgramme.Mvc.Areas.Admin.Controllers;

[Area(DefaultRoles.Admin)]
[Authorize(Roles = DefaultRoles.Admin)]
public class OfficersController(IUnitOfWork unitOfWork) : Controller
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpGet]
    public async Task<IActionResult> Index(
        CancellationToken cancellationToken = default)
    {
        var officers = await _unitOfWork.Officers.GetAllAsync(
            include: q => q.Include(x => x.Rank),
            orderByExpression: x => x.CreatedDate,
            order: OrderBy.Descending,
            cancellationToken: cancellationToken);

        return View(officers);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(
        int? id,
        CancellationToken cancellationToken = default)
    {
        var vm = new OfficerVm();

        await FillRanks(vm, cancellationToken);

        if (id is null)
            return View(vm);

        var officer = await _unitOfWork.Officers.GetAsync(
            x => x.Id == id,
            cancellationToken: cancellationToken);

        if (officer is null)
            return NotFound();

        vm.Id = officer.Id;
        vm.FullName = officer.FullName;
        vm.RankId = officer.RankId;
        vm.IsActive = officer.IsActive;

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(
        OfficerVm vm,
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            await FillRanks(vm, cancellationToken);
            return View(nameof(Edit), vm);
        }

        Officer officer;

        if (vm.Id == 0)
        {
            officer = new Officer();
            TempData["success"] = "تم اضافة العنصر بنجاح";
        }
        else
        {
            officer = await _unitOfWork.Officers.GetAsync(
                x => x.Id == vm.Id,
                cancellationToken: cancellationToken);

            if (officer is null)
                return NotFound();

            TempData["success"] = "تم تعديل العنصر بنجاح";
        }

        officer.FullName = vm.FullName;
        officer.RankId = vm.RankId;
        officer.IsActive = vm.IsActive;

        await _unitOfWork.Officers.SaveAsync(
            officer,
            User.GetUserFullname()!,
            cancellationToken);

        await _unitOfWork.CompleteAsync(cancellationToken);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.Officers.GetAsync(
            x => x.Id == id,
            cancellationToken: cancellationToken);

        if (entity is null)
        {
            return Json(new
            {
                success = false,
                message = "العنصر غير موجود"
            });
        }
       
        try
        {
            _unitOfWork.Officers.Remove(entity);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return Json(new
            {
                success = true,
                message = Messages.SuccessRemoveItem
            });
        }
        catch
        {
            return Json(new
            {
                success = false,
                message = Messages.ErrorRemoveItem
            });
        }

    }

    private async Task FillRanks(
        OfficerVm vm,
        CancellationToken cancellationToken)
    {
        vm.Ranks = (await _unitOfWork.Ranks.GetAllAsync(
            cancellationToken: cancellationToken))
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            })
            .ToList();
    }
}