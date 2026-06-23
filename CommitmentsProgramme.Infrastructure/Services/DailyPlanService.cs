//using Microsoft.AspNetCore.Mvc.Rendering;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using CommitmentsProgramme.Application.DTOs;

//namespace CommitmentsProgramme.Infrastructure.Services;

//public class DailyPlanService : IDailyPlanService
//{
//  private readonly IUnitOfWork _uow;

//  public DailyPlanService(IUnitOfWork uow)
//  {
//    _uow = uow;
//  }

//  public async Task<DailyPlanDTO> GetForEditAsync(int? id)
//  {
//    var vm = new DailyPlanDTO();

//    vm.SeniorOfficers = await GetOfficers(8, 12); // عقيد -> لواء
//    vm.DutyOfficers = await GetOfficers(5, 7);    // ملازم -> نقيب

//    vm.Priorities = await _uow.Repository<Priority>()
//        .GetAll().Select(x => new SelectListItem
//        {
//          Value = x.Id.ToString(),
//          Text = x.Name
//        }).ToListAsync();

//    vm.CommitmentTypes = await _uow.Repository<CommitmentType>()
//        .GetAll().Select(x => new SelectListItem
//        {
//          Value = x.Id.ToString(),
//          Text = x.Name
//        }).ToListAsync();

//    vm.Branches = await _uow.Repository<Branch>()
//        .GetAll().Select(x => new SelectListItem
//        {
//          Value = x.Id.ToString(),
//          Text = x.Name
//        }).ToListAsync();

//    if (id == null)
//    {
//      vm.PlanDate = DateOnly.FromDateTime(DateTime.Today);
//      return vm;
//    }

//    var plan = await _uow.Repository<DailyPlan>()
//        .GetAll()
//        .Include(x => x.Commitments)
//        .FirstAsync(x => x.Id == id);

//    vm.Id = plan.Id;
//    vm.PlanDate = plan.PlanDate;
//    vm.SeniorOfficerId = plan.SeniorOfficerId;
//    vm.DutyOfficerId = plan.DutyOfficerId;

//    return vm;
//  }

//  public async Task SaveAsync(DailyPlanVm vm)
//  {
//    DailyPlan plan;

//    if (vm.Id == null)
//    {
//      plan = new DailyPlan();
//      await _uow.Repository<DailyPlan>().AddAsync(plan);
//    }
//    else
//    {
//      plan = await _uow.Repository<DailyPlan>()
//          .GetAll()
//          .Include(x => x.Commitments)
//          .FirstAsync(x => x.Id == vm.Id);

//      _uow.Repository<Commitment>().DeleteRange(plan.Commitments);
//    }

//    plan.PlanDate = vm.PlanDate;
//    plan.SeniorOfficerId = vm.SeniorOfficerId;
//    plan.DutyOfficerId = vm.DutyOfficerId;

//    plan.Commitments = vm.Commitments.Select(c => new Commitment
//    {
//      Title = c.Title,
//      Time = c.Time,
//      CommitmentTypeId = c.CommitmentTypeId,
//      PriorityId = c.PriorityId,
//      Location = c.Location,
//      Attendance = c.Attendance,
//      Notes = c.Notes,

//      Branches = c.BranchIds.Select(b => new CommitmentBranch
//      {
//        BranchId = b
//      }).ToList()
//    }).ToList();

//    await _uow.SaveChangesAsync();
//  }

//  private async Task<List<SelectListItem>> GetOfficers(int fromRank, int toRank)
//  {
//    return await _uow.Repository<Officer>()
//        .GetAll()
//        .Where(x => x.RankId >= fromRank && x.RankId <= toRank)
//        .Select(x => new SelectListItem
//        {
//          Value = x.Id.ToString(),
//          Text = x.FullName
//        }).ToListAsync();
//  }
//}