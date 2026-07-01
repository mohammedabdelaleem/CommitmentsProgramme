using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommitmentsProgramme.Mvc.Services;

public class DailyPlanService(IUnitOfWork unitOfWork) : IDailyPlanService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<DailyPlanDetailsVm> GetDetailsAsync(DailyPlanRequestVM requestVM, CancellationToken cancellationToken = default)
    {
        var plan = await _unitOfWork.DailyPlans.GetAsync(
            x => x.Id == requestVM.Id || x.PlanDate == requestVM.Date,
            include: q => q

                .Include(x => x.SeniorOfficer)
                    .ThenInclude(x => x.Rank)

                .Include(x => x.DutyOfficer)
                    .ThenInclude(x => x.Rank)

                .Include(x => x.Commitments)
                    .ThenInclude(x => x.CommitmentType)

                .Include(x => x.Commitments)
                    .ThenInclude(x => x.Priority)

                .Include(x => x.Commitments)
                    .ThenInclude(x => x.Place)

                .Include(x => x.Commitments)
                    .ThenInclude(x => x.CommitmentBranches)
                        .ThenInclude(x => x.Branch)

                .Include(x => x.Commitments)
                    .ThenInclude(x => x.CommitmentsAttendances)
                        .ThenInclude(x => x.Attendance)
        , cancellationToken: cancellationToken);

        if (plan is null)
        {
            //throw new InvalidOperationException(
            //    $"DailyPlan with Id {(requestVM.Id > 0 ? requestVM.Id : 1999)} was not found.");

            return new DailyPlanDetailsVm();
        }

        var vm = new DailyPlanDetailsVm
        {
            Id = plan.Id,

            PlanDate = plan.PlanDate,

            SeniorOfficer = new OfficerInfoVm
            {
                RankName = plan.SeniorOfficer.Rank.Name,
                FullName = plan.SeniorOfficer.FullName,
                PhoneNumber = plan.SeniorOfficer.PhoneNumber
            },

            DutyOfficer = new OfficerInfoVm
            {
                RankName = plan.DutyOfficer.Rank.Name,
                FullName = plan.DutyOfficer.FullName,
                PhoneNumber = plan.DutyOfficer.PhoneNumber
            }
        };

        var commitments = plan.Commitments
            .OrderBy(x => x.Time)
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

                BranchNames = c.CommitmentBranches
                    .Select(x => x.Branch.Name)
                    .ToList(),

                AttendanceIds = c.CommitmentsAttendances
                    .Select(x => x.AttendanceId)
                    .ToList(),

                AttendanceNames = c.CommitmentsAttendances
                    .Select(x => x.Attendance.Title)
                    .ToList()
            })
            .ToList();

        // todo: don't use fiixed data
        vm.OutsideCommitments = commitments
            .Where(x => x.CommitmentTypeId == 4)
            .ToList();

        vm.InsideCommitments = commitments
            .Where(x => x.CommitmentTypeId == 3)
            .ToList();

        return vm;
    }
    public async Task<DailyPlanVm> GetForEditAsync(int id, CancellationToken cancellationToken = default)
    {
        var vm = new DailyPlanVm();

        // ==========================
        // Load lookup lists
        // ==========================

        vm.SeniorOfficers = await GetOfficers(1, 8);

        //vm.DutyOfficers = await GetOfficers(5, 7);
        vm.DutyOfficers =
            (await _unitOfWork.Officers
            .GetAllAsync(x => x.RankId == 9 || x.RankId == 13 || x.RankId == 16 || x.RankId == 21, cancellationToken: cancellationToken))
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FullName
            })
            .ToList(); ;

        vm.Priorities = (await _unitOfWork.Priorities.GetAllAsync(cancellationToken: cancellationToken))
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            })
            .ToList();

        vm.CommitmentTypes = (await _unitOfWork.CommitmentTypes.GetAllAsync(cancellationToken: cancellationToken))
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            })
            .ToList();

        var branches = await _unitOfWork.Branches.GetAllAsync(x => x.IsActive, cancellationToken: cancellationToken) ?? [];

        vm.Branches = branches
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            })
            .ToList();

        vm.Attendances = (await _unitOfWork.Attendances.GetAllAsync(x => !x.IsDeleted, cancellationToken: cancellationToken))
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Title
            })
            .ToList();

        vm.Places = (await _unitOfWork.Places.GetAllAsync(x => !x.IsDeleted, cancellationToken: cancellationToken))
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
                    .ThenInclude(x => x.Priority),

            cancellationToken: cancellationToken);

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
            .OrderBy(x => x.Time)
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

         BranchNames = c.CommitmentBranches
            .Select(x => x.Branch.Name)
            .ToList(),

         AttendanceIds = c.CommitmentsAttendances
            .Select(x => x.AttendanceId)
            .ToList(),

         AttendanceNames = c.CommitmentsAttendances
            .Select(x => x.Attendance.Title)
            .ToList(),
     })
             .ToList();

        return vm;
    }

    public async Task<DailyPlanPrintVm> GetForPrintAsync(int id, CancellationToken cancellationToken = default)
    {
        var plan = await _unitOfWork.DailyPlans.GetAsync(
            x => x.Id == id,
            include: q => q

                .Include(x => x.SeniorOfficer)
                    .ThenInclude(x => x.Rank)

                .Include(x => x.DutyOfficer)
                    .ThenInclude(x => x.Rank)

                .Include(x => x.Commitments)
                    .ThenInclude(x => x.CommitmentType)

                .Include(x => x.Commitments)
                    .ThenInclude(x => x.Place)

                .Include(x => x.Commitments)
                    .ThenInclude(x => x.CommitmentBranches)
                        .ThenInclude(x => x.Branch)

                .Include(x => x.Commitments)
                    .ThenInclude(x => x.CommitmentsAttendances)
                        .ThenInclude(x => x.Attendance),

            cancellationToken: cancellationToken
        );

        if (plan is null)
        {
            throw new InvalidOperationException(
                $"Daily Plan {id} was not found.");
        }

        var vm = new DailyPlanPrintVm
        {
            PlanDate = plan.PlanDate,

            SeniorOfficerName = plan.SeniorOfficer.FullName,
            SeniorOfficerRank = plan.SeniorOfficer.Rank.Name,
            SeniorOfficerPhone = plan.SeniorOfficer.PhoneNumber,

            DutyOfficerName = plan.DutyOfficer.FullName,
            DutyOfficerRank = plan.DutyOfficer.Rank.Name,
            DutyOfficerPhone = plan.DutyOfficer.PhoneNumber
        };

        vm.OutsideCommitments = plan.Commitments
            .OrderBy(x => x.Time)
            .Where(x => x.CommitmentType.Name.Contains("خار"))
            .Select(c => new CommitmentPrintVm
            {
                Title = c.Title,

                PlaceName = c.Place.Name,

                Time = c.Time,

                Notes = c.Notes,

                Branches = c.CommitmentBranches
                    .Select(x => x.Branch.Name)
                    .ToList(),

                Attendances = c.CommitmentsAttendances
                    .Select(x => x.Attendance.Title)
                    .ToList()
            })
            .OrderBy(x => x.Time)
            .ToList();

        vm.InsideCommitments = plan.Commitments
            .OrderBy(x => x.Time)
            .Where(x => x.CommitmentType.Name.Contains("داخ"))
            .Select(c => new CommitmentPrintVm
            {
                Title = c.Title,

                PlaceName = c.Place.Name,

                Time = c.Time,

                Notes = c.Notes,

                Branches = c.CommitmentBranches
                    .Select(x => x.Branch.Name)
                    .ToList(),

                Attendances = c.CommitmentsAttendances
                    .Select(x => x.Attendance.Title)
                    .ToList()
            })
            .OrderBy(x => x.Time)
            .ToList();

        return vm;
    }

    public async Task SaveAsync(DailyPlanVm vm, string userFullName, CancellationToken cancellationToken = default)
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
                userFullName,
                cancellationToken);
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
                        .ThenInclude(x => x.CommitmentsAttendances),

                cancellationToken: cancellationToken);

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
                userFullName,
                cancellationToken);
        }

        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    private async Task<List<SelectListItem>> GetOfficers(
        int fromRank,
        int toRank,
        CancellationToken cancellationToken = default)
    {
        return (await _unitOfWork.Officers.GetAllAsync(
                x => x.RankId >= fromRank &&
                     x.RankId <= toRank &&
                     x.IsActive,
                cancellationToken: cancellationToken))
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FullName
            })
            .ToList();
    }

}