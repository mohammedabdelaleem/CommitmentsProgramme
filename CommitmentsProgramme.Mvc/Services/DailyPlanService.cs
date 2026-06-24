using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommitmentsProgramme.Mvc.Services;

public class DailyPlanService(IUnitOfWork unitOfWork) : IDailyPlanService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<DailyPlanVm> GetForEditAsync(int id)
    {
        var vm = new DailyPlanVm();

        // ==========================
        // Load lookup lists
        // ==========================

        vm.SeniorOfficers = await GetOfficers(1,8);

        //vm.DutyOfficers = await GetOfficers(5, 7);
        vm.DutyOfficers = 
            (await _unitOfWork.Officers
            .GetAllAsync(x => x.RankId == 9 || x.RankId == 13 || x.RankId == 16 || x.RankId == 21))
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FullName
            })
            .ToList(); ;

        vm.Priorities = (await _unitOfWork.Priorities.GetAllAsync())
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            })
            .ToList();

        vm.CommitmentTypes = (await _unitOfWork.CommitmentTypes.GetAllAsync())
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            })
            .ToList();

        var branches = await _unitOfWork.Branches.GetAllAsync(x => x.IsActive) ?? [];

        vm.Branches = branches
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            })
            .ToList();

        vm.Attendances = (await _unitOfWork.Attendances.GetAllAsync(x => !x.IsDeleted))
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Title
            })
            .ToList();

        vm.Places = (await _unitOfWork.Places.GetAllAsync(x => !x.IsDeleted))
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            })
            .ToList();

        // ==========================
        // New Daily Plan
        // ==========================

        if (id == 0)
        {
            vm.PlanDate = DateOnly.FromDateTime(DateTime.Today);
            return vm;
        }

        // ==========================
        // Edit Existing Daily Plan
        // ==========================

        var plan = await _unitOfWork.DailyPlans.GetAsync(
            x => x.Id == id,
            include: q => q
                .Include(x => x.Commitments)
                    .ThenInclude(x => x.CommitmentBranches)
                        

                .Include(x => x.Commitments)
                    .ThenInclude(x => x.CommitmentsAttendances)

                .Include(x => x.Commitments)
                    .ThenInclude(x => x.Place)

                .Include(x => x.Commitments)
                    .ThenInclude(x => x.CommitmentType)

                .Include(x => x.Commitments)
                    .ThenInclude(x => x.Priority));

        if (plan is null)
        {
            throw new InvalidOperationException(
                $"DailyPlan with Id {id} was not found.");
        }

        vm.Id = plan.Id;
        vm.PlanDate = plan.PlanDate;
        vm.SeniorOfficerId = plan.SeniorOfficerId;
        vm.DutyOfficerId = plan.DutyOfficerId;

        vm.Commitments = plan.Commitments
     .Select(c => new CommitmentVm
     {
         Id = c.Id,

         Title = c.Title,

         Time = c.Time,

         CommitmentTypeId = c.CommitmentTypeId,

         CommitmentTypeName = c.CommitmentType.Name,

         PriorityId = c.PriorityId,

         PriorityName = c.Priority.Name,

         PlaceId = c.PlaceId,

         PlaceName = c.Place.Name,

         Notes = c.Notes,

         BranchIds = c.CommitmentBranches
             .Select(x => x.BranchId)
             .ToList(),

         AttendanceIds = c.CommitmentsAttendances
             .Select(x => x.AttendanceId)
             .ToList()
     })
     .ToList();

        return vm;
    }

    public async Task SaveAsync(DailyPlanVm vm, string userFullName)
    {
        DailyPlan plan;

        // ==========================
        // CREATE
        // ==========================

        if (vm.Id == 0)
        {
            plan = new DailyPlan
            {
                PlanDate = vm.PlanDate,
                SeniorOfficerId = vm.SeniorOfficerId,
                DutyOfficerId = vm.DutyOfficerId
            };

            foreach (var c in vm.Commitments ?? [])
            {
                var commitment = new Commitment
                {
                    

                    Title = c.Title,

                    Time = c.Time,

                    CommitmentTypeId = c.CommitmentTypeId,

                    PriorityId = c.PriorityId,

                    PlaceId = c.PlaceId,

                    Notes = c.Notes,

                    CommitmentBranches = (c.BranchIds ?? [])
                                 .Select(branchId => new CommitmentBranch
                                 {
                                     BranchId = branchId
                                 })
                                 .ToList(),

                    CommitmentsAttendances = (c.AttendanceIds ?? [])
         .Select(attendanceId => new CommitmentsAttendances
         {
             AttendanceId = attendanceId
         })
         .ToList()
                };



                plan.Commitments.Add(commitment);
            }

            await _unitOfWork.DailyPlans.SaveAsync(
                plan,
                userFullName);
        }

        // ==========================
        // UPDATE
        // ==========================

        else
        {
            plan = await _unitOfWork.DailyPlans.GetAsync(
                x => x.Id == vm.Id,
                include: q => q

                    .Include(x => x.Commitments)
                        .ThenInclude(x => x.CommitmentBranches)

                    .Include(x => x.Commitments)
                        .ThenInclude(x => x.CommitmentsAttendances));

            if (plan is null)
            {
                throw new InvalidOperationException(
                    $"DailyPlan with Id {vm.Id} was not found.");
            }

            // Update header

            plan.PlanDate = vm.PlanDate;
            plan.SeniorOfficerId = vm.SeniorOfficerId;
            plan.DutyOfficerId = vm.DutyOfficerId;

            // Remove old commitments

            var commitmentsToDelete = plan.Commitments.ToList();

            _unitOfWork.Commitments.RemoveRange(commitmentsToDelete);

            plan.Commitments.Clear();

            // Recreate commitments

            foreach (var c in vm.Commitments ?? [])
            {
                var commitment = new Commitment
                {
                    Title = c.Title,

                    Time = c.Time,

                    CommitmentTypeId = c.CommitmentTypeId,

                    PriorityId = c.PriorityId,

                    PlaceId = c.PlaceId,

                    Notes = c.Notes,

                    CommitmentBranches = c.BranchIds
                        .Select(branchId => new CommitmentBranch
                        {
                            BranchId = branchId
                        })
                        .ToList(),

                    CommitmentsAttendances = c.AttendanceIds
                        .Select(attendanceId => new CommitmentsAttendances
                        {
                            AttendanceId = attendanceId
                        })
                        .ToList()
                };

                plan.Commitments.Add(commitment);
            }

            await _unitOfWork.DailyPlans.SaveAsync(
                plan,
                userFullName);
        }

        await _unitOfWork.CompleteAsync();
    }
    private async Task<List<SelectListItem>> GetOfficers(
        int fromRank,
        int toRank)
    {
        return (await _unitOfWork.Officers.GetAllAsync(
                x => x.RankId >= fromRank &&
                     x.RankId <= toRank &&
                     x.IsActive))
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FullName
            })
            .ToList();
    }
}