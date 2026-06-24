using CommitmentsProgramme.Utilities.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace CommitmentsProgramme.Mvc.Controllers
{
    [Authorize]
    public class DailyPlanController(IDailyPlanService dailyPlanService,
        IUnitOfWork unitOfWork) : Controller
    {
        private readonly IDailyPlanService _dailyPlanService = dailyPlanService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IActionResult> Index()
        {
            //var userId = User.GetUserId();
            // Load all daily plans (light data only)
            var plans = await _unitOfWork.DailyPlans.GetAllAsync(
                include: q => q
                    .Include(x => x.SeniorOfficer)
                    .Include(x => x.DutyOfficer)
                    .Include(x => x.Commitments));

            var vm = plans.Select(p => new DailyPlanListVm
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
        public async Task<IActionResult> Create(int? id)
        {
            if(id == null)
            {
                var vm = await _dailyPlanService.GetForEditAsync(0);

                return View(vm);
            }
            else
            {
                var vm = await _dailyPlanService.GetForEditAsync(id.Value);

                // calculate the current items
                ViewBag.CommitmentIndex = vm.Commitments.Count;

                return View("Create", vm);
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DailyPlanVm vm)
        {
            if(vm.Id == 0)
            {
                if (!ModelState.IsValid)
                {
                    vm = await _dailyPlanService.GetForEditAsync(0);
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
                    vm = await _dailyPlanService.GetForEditAsync(vm.Id);

                    return View("Create", vm);
                }

                var userId = User.GetUserId();
                await _dailyPlanService.SaveAsync(vm, userId);

                return RedirectToAction(nameof(Index));
            }
          
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var plan = await _unitOfWork.DailyPlans.GetAsync(
                x => x.Id == id,
                include: q => q
                    .Include(x => x.Commitments)
                        .ThenInclude(x => x.CommitmentBranches)
                    .Include(x => x.Commitments)
                        .ThenInclude(x => x.CommitmentsAttendances));

            if (plan is null)
            {
                return NotFound();
            }


            foreach (var commitment in plan.Commitments)
            {
                commitment.CommitmentBranches.Clear();
                commitment.CommitmentsAttendances.Clear();
            }
            _unitOfWork.Commitments.RemoveRange(plan.Commitments);
            _unitOfWork.DailyPlans.Remove(plan);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}
