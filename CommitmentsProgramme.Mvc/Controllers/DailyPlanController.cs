using CommitmentsProgramme.Utilities.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;

namespace CommitmentsProgramme.Mvc.Controllers
{
    [Authorize]
    public class DailyPlanController(IDailyPlanService dailyPlanService,
        IUnitOfWork unitOfWork) : Controller
    {
        private readonly IDailyPlanService _dailyPlanService = dailyPlanService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IActionResult> Index(DateOnly? date)
        {
            List<DailyPlan> plans;
            if (date is null)
            {
                plans = await _unitOfWork.DailyPlans.GetAllAsync(

              include: q => q
                  .Include(x => x.SeniorOfficer)
                  .Include(x => x.DutyOfficer)
                  .Include(x => x.Commitments),

              orderByExpression:x=>x.PlanDate,
              order:OrderBy.Descending);
            }
            else
            {
                var requestDateMonth = date.Value.Month;
                var requestDateYear = date.Value.Year;

                plans = await _unitOfWork.DailyPlans.GetAllAsync(
             x => x.PlanDate.Month == requestDateMonth && x.PlanDate.Year == requestDateYear,
             include: q => q
                 .Include(x => x.SeniorOfficer)
                 .Include(x => x.DutyOfficer)
                 .Include(x => x.Commitments),

              orderByExpression: x => x.PlanDate,
              order: OrderBy.Descending);
            }


            var vm = plans?.Select(p => new DailyPlanListVm
            {
                Id = p.Id,
                PlanDate = p.PlanDate,
                SeniorOfficerName = p.SeniorOfficer.FullName,
                DutyOfficerName = p.DutyOfficer.FullName,
                CommitmentsCount = p.Commitments.Count
            }).ToList();

            return View(vm);
        }


        [HttpGet]
        public async Task<IActionResult> Details(DailyPlanRequestVM requestVM)
        {
            DailyPlanDetailsVm vm = new();
            if (requestVM.Id > 0)
                vm = await _dailyPlanService.GetDetailsAsync(requestVM);
            else if (requestVM.Date is not null)
                vm = await _dailyPlanService.GetDetailsAsync(requestVM);

            return View(vm);
        }


        [HttpGet]
        public async Task<IActionResult> Print(int id)
        {
            DailyPlanPrintVm vm = await _dailyPlanService.GetForPrintAsync(id);
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                var vm = await _dailyPlanService.GetForEditAsync(0);

                return View(vm);
            }
            else
            {
                var vm = await _dailyPlanService.GetForEditAsync(id.Value);

                // calculate the current items
                ViewBag.CommitmentIndex = vm.Commitments.Count;

                return View(vm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DailyPlanVm vm)
        {
            if (vm.Id == 0)
            {
                if (!ModelState.IsValid)
                {
                    //vm = await _dailyPlanService.GetForEditAsync(0);
                    return View(vm);
                }

                var userId = User.GetUserId();
                await _dailyPlanService.SaveAsync(vm, userId);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    //vm = await _dailyPlanService.GetForEditAsync(vm.Id);

                    return View("Create", vm);
                }

                var userId = User.GetUserId();
                await _dailyPlanService.SaveAsync(vm, userId);

                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var plan = await _unitOfWork.DailyPlans.GetAsync(
                x => x.Id == id,
                include: q => q
                    .Include(x => x.Commitments)
                        .ThenInclude(x => x.CommitmentBranches)
                    .Include(x => x.Commitments)
                        .ThenInclude(x => x.CommitmentsAttendances),
                cancellationToken: cancellationToken);

            if (plan is null)
            {
                return Json(new { success = false, message = Messages.ItemNotFound });
            }



            try
            {

                foreach (var commitment in plan.Commitments)
                {
                    commitment.CommitmentBranches.Clear();
                    commitment.CommitmentsAttendances.Clear();
                }

                _unitOfWork.Commitments.RemoveRange(plan.Commitments);
                _unitOfWork.DailyPlans.Remove(plan);
                int result = await _unitOfWork.CompleteAsync(cancellationToken);

                if (result > 0)
                {
                    return Json(new
                    {
                        success = true,
                        message = Messages.SuccessRemoveItem
                    });
                }

                else
                {
                    return Json(new
                    {
                        success = false,
                        message = Messages.ErrorRemoveItem
                    });
                }
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
