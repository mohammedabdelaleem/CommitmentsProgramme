using CommitmentsProgramme.Utilities.Extensions;
using Microsoft.AspNetCore.Authorization;
using static CommitmentsProgramme.Utilities.Abstractions.Consts.SharedData;

namespace CommitmentsProgramme.Mvc.Areas.Admin.Controllers
{


    [Area(DefaultRoles.Admin)]
    [Authorize(Roles = DefaultRoles.Admin)]
    public class PlacesController(IUnitOfWork unitOfWork) : Controller
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.Places.GetAllAsync(orderByExpression: x => x.CreatedDate, order: OrderBy.Descending, cancellationToken: cancellationToken);
            return View(nameof(Index), entities);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken = default)
        {
            if (id == null)
                return View(nameof(Edit), new Place());

            else
            {
                var entity = await _unitOfWork.Places.GetAsync(x => x.Id == id!.Value, cancellationToken: cancellationToken);
                return View(nameof(Edit), entity);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Place entity, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Edit), entity);
            }

            var username = User.GetUserFullname();

            if (entity.Id == 0)
            {
                TempData["success"] = "تم اضافة المكان بنجاح ";
            }

            else
            {
                TempData["success"] = "تم تعديل المكان بنجاح";
            }

            await _unitOfWork.Places.SaveAsync(entity, username!, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);


            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.Places.GetAsync(
                x => x.Id == id,
                cancellationToken: cancellationToken);

            if (entity is null)
            {
                return Json(new
                {
                    success = false,
                    message = Messages.ItemNotFound
                });
            }

            try
            {
                _unitOfWork.Places.Remove(entity);
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

    }
}
