using CommitmentsProgramme.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommitmentsProgramme.Mvc.Services;

public class ShiftsService(IUnitOfWork unitOfWork) : IShiftsService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task AddShift(ShiftAddVm shift, string name)
    {
        var entity = shift.Adapt<Shifts>();
        await _unitOfWork.shifts
            .AddAsync(entity, name);

        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteShifts(int id)
    {
        var entity =  await _unitOfWork.shifts

            .GetAsync(x => x.Id == id);

        if (entity == null)
            return;

        _unitOfWork.shifts
            .Remove(entity);

        await _unitOfWork.CompleteAsync();
    }

    public async Task EditShifts(Shiftvm vm, string name)
    {
        var entity =         await _unitOfWork.shifts

            .GetAsync(x => x.Id == vm.Id);

        if (entity == null)
            return;

        entity.Name = vm.Name;
        entity.Phone = vm.Phone;
        entity.shiftRank = (ShiftRank)vm.EnumSelect;
entity.dateOnly = vm.dateOnly;
        await _unitOfWork.shifts
            .SaveAsync(entity, name);

        await _unitOfWork.CompleteAsync();
    }

    public async Task<List<Shiftvm>> GetAllShift(DateOnly? date)
    {
        List<Shifts> shifts;

        if (date.HasValue)
        {
            shifts =        await _unitOfWork.shifts

                .GetAllAsync(x => x.dateOnly == date.Value);
        }
        else
        {
            shifts =         await _unitOfWork.shifts

                .GetAllAsync();
        }

        return shifts.Adapt<List<Shiftvm>>();
    }

    public async Task<Shifts?> GetByIdAsync(int id)
    {
        return       await _unitOfWork.shifts

            .GetAsync(x => x.Id == id);
    }

    public async Task<List<SelectListItem>> GetRanksShift()
    {
        return await Task.FromResult(
            Enum.GetValues(typeof(ShiftRank))
                .Cast<ShiftRank>()
                .Select(x => new SelectListItem
                {
                    Text = x.ToString(),
                    Value = ((int)x).ToString()
                })
                .ToList());
    }
}