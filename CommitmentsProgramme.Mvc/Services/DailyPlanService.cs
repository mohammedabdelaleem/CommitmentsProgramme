using CommitmentsProgramme.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CommitmentsProgramme.Mvc.Services;

public class DailyPlanService(IUnitOfWork unitOfWork) : IDailyPlanService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<DailyPlanVm> GetForEditAsync(int? id)
    {
        var vm = new DailyPlanVm();

        // ==========================
        // Load lookup lists
        // ==========================

        vm.SeniorOfficers = await GetOfficers(8, 12);

        vm.DutyOfficers = await GetOfficers(5, 7);

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

        if (!id.HasValue)
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
                    .ThenInclude(x => x.Branches)
                .Include(x => x.Commitments)
                    .ThenInclude(x => x.Attendances)
                .Include(x => x.Commitments)
                    .ThenInclude(x => x.Place));

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

                PriorityId = c.PriorityId,

                PlaceId = c.PlaceId,

                Notes = c.Notes,

                BranchIds = c.Branches
                    .Select(x => x.Id)
                    .ToList(),

                AttendanceIds = c.Attendances
                    .Select(x => x.Id)
                    .ToList()
            })
            .ToList();

        return vm;
    }

    public async Task SaveAsync(DailyPlanVm vm, string userFullName)
    {
        // ==========================
        // Load lookup tables once
        // Avoid N+1 Queries
        // ==========================

        var branchesLookup = (await _unitOfWork.Branches.GetAllAsync())
            .ToDictionary(x => x.Id);

        var attendancesLookup = (await _unitOfWork.Attendances.GetAllAsync())
            .ToDictionary(x => x.Id);

        DailyPlan plan;

        // ==========================
        // Create
        // ==========================

        if (vm.Id is null || vm.Id == 0)
        {
            plan = new DailyPlan();

            plan.PlanDate = vm.PlanDate;
            plan.SeniorOfficerId = vm.SeniorOfficerId;
            plan.DutyOfficerId = vm.DutyOfficerId;

            foreach (var c in vm.Commitments)
            {
                var commitment = new Commitment
                {
                    Id= c.Id,
                    Title = c.Title,

                    Time = c.Time,

                    CommitmentTypeId = c.CommitmentTypeId,

                    PriorityId = c.PriorityId,

                    PlaceId = c.PlaceId,

                    Notes = c.Notes,

                    Branches = c.BranchIds
                        .Where(branchesLookup.ContainsKey)
                        .Select(id => branchesLookup[id])
                        .ToList(),

                    Attendances = c.AttendanceIds
                        .Where(attendancesLookup.ContainsKey)
                        .Select(id => attendancesLookup[id])
                        .ToList()
                };

                plan.Commitments.Add(commitment);
            }

            await _unitOfWork.DailyPlans.SaveAsync(
                plan,
               userFullName);
        }

        // ==========================
        // Update
        // ==========================

        else
        {
            plan = await _unitOfWork.DailyPlans.GetAsync(
                x => x.Id == vm.Id,
                include: q => q
                    .Include(x => x.Commitments)
                        .ThenInclude(x => x.Branches)
                    .Include(x => x.Commitments)
                        .ThenInclude(x => x.Attendances));

            if (plan is null)
            {
                throw new InvalidOperationException(
                    $"DailyPlan with Id {vm.Id} was not found.");
            }

            plan.PlanDate = vm.PlanDate;
            plan.SeniorOfficerId = vm.SeniorOfficerId;
            plan.DutyOfficerId = vm.DutyOfficerId;

            // Remove old commitments
            _unitOfWork.Commitments.RemoveRange(plan.Commitments);

            plan.Commitments.Clear();

            // Recreate commitments
            foreach (var c in vm.Commitments)
            {
                var commitment = new Commitment
                {
                    Id = c.Id,  
                    Title = c.Title,

                    Time = c.Time,

                    CommitmentTypeId = c.CommitmentTypeId,

                    PriorityId = c.PriorityId,

                    PlaceId = c.PlaceId,

                    Notes = c.Notes,

                    Branches = c.BranchIds
                        .Where(branchesLookup.ContainsKey)
                        .Select(id => branchesLookup[id])
                        .ToList(),

                    Attendances = c.AttendanceIds
                        .Where(attendancesLookup.ContainsKey)
                        .Select(id => attendancesLookup[id])
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