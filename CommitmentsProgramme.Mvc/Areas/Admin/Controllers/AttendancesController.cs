using CommitmentsProgramme.Utilities.Extensions;
using Microsoft.AspNetCore.Authorization;
using static CommitmentsProgramme.Utilities.Abstractions.Consts.SharedData;

namespace CommitmentsProgramme.Mvc.Areas.Admin.Controllers
{


    [Area(DefaultRoles.Admin)]
    [Authorize(Roles = DefaultRoles.Admin)]
    public class AttendancesController(IUnitOfWork unitOfWork) : Controller
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {
            var attendences = await _unitOfWork.Attendances.GetAllAsync(orderByExpression:x=>x.CreatedDate, order:OrderBy.Descending,cancellationToken: cancellationToken);
            return View(nameof(Index), attendences);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken = default)
        {
            if (id == null)
                 return View(nameof(Edit), new Attendance());
            
            else
            {
                var attendance = await _unitOfWork.Attendances.GetAsync(x=>x.Id== id!.Value, cancellationToken: cancellationToken);
                return View(nameof(Edit), attendance);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Attendance attendance,CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Edit), attendance);
            }

            var username = User.GetUserFullname();
            if (attendance.Id == 0)
            {
                await _unitOfWork.Attendances.SaveAsync(attendance, username!,cancellationToken);
                TempData["Created"] = "تم اضافة الحضور بنجاح";
            }

            else
            {
                
                await _unitOfWork.Attendances.SaveAsync(attendance, username!,cancellationToken);
                TempData["Updated"] = "تم تعديل الحضور بنجاح";
            }
            await _unitOfWork.CompleteAsync(cancellationToken);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var attendance = await _unitOfWork.Attendances.GetAsync(
                x => x.Id == id,
                cancellationToken: cancellationToken);

            if (attendance is null)
            {
                return Json(new
                {
                    success = false,
                    message = "الحضور غير موجود"
                });
            }

            _unitOfWork.Attendances.Remove(attendance);

            await _unitOfWork.CompleteAsync(cancellationToken);

            return Json(new
            {
                success = true,
                message = "تم حذف الحضور بنجاح"
            });
        }

    }
}
