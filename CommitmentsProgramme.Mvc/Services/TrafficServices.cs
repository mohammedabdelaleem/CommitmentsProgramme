using CommitmentsProgramme.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CommitmentsProgramme.Mvc.Services;

public class TrafficServices(IUnitOfWork unitOfWork) : ITrafficServices
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

  

    public async Task AddTraffic(TrafficPlanAddVm vm, string name)
    {
    var entity = new TrafficPlane
    {
        dateOnly = vm.DateOnly,

        TrafficPlaces = vm.PlaceIds.Select(id => new TrafficPlace
        {
            PlaceId = id
        }).ToList(),

        TrafficOfficer = vm.OfficerIds.Select(id => new TrafficOfficer
        {
            OfficerId = id ,
            
        }).ToList()


       
    };

 
    await _unitOfWork.TrafficPlane.SaveAsync(entity, name); 
        await _unitOfWork.CompleteAsync(); 

    
       }

    public async Task DeleteTraffic(int id)
{
    var entity = await _unitOfWork.TrafficPlane.GetAsync(
        t => t.Id == id,
        include: q => q
            .Include(t => t.TrafficOfficer)
            .Include(t => t.TrafficPlaces)
    );

    if (entity == null)
        throw new Exception("Not found");

    _unitOfWork.trafficOfficer.RemoveRange(entity.TrafficOfficer);
    _unitOfWork.trafficPlace.RemoveRange(entity.TrafficPlaces);
    _unitOfWork.TrafficPlane.Remove(entity);

    await _unitOfWork.CompleteAsync(); // 🔥 مهم جدًا
}

  public async Task EditTraffic(TrafficPlanEditVm vm, string name)
{
    var entity = await _unitOfWork.TrafficPlane.GetAsync(
        t => t.Id == vm.Id,
        include: q => q
            .Include(t => t.TrafficOfficer)
            .Include(t => t.TrafficPlaces)
    );

    if (entity == null)
        throw new Exception("Not found");

    // update scalar
    entity.dateOnly = vm.DateOnly;

    // ❌ REMOVE OLD CHILDREN PROPERLY
    _unitOfWork.trafficOfficer.RemoveRange(entity.TrafficOfficer);
    _unitOfWork.trafficPlace.RemoveRange(entity.TrafficPlaces);

    // ⚠️ IMPORTANT: flush changes before adding new ones
    await _unitOfWork.CompleteAsync();

    // ✅ ADD NEW CHILDREN
    entity.TrafficPlaces = vm.PlaceIds.Select(id => new TrafficPlace
    {
        PlaceId = id,
        TrafficId = entity.Id
    }).ToList();

    entity.TrafficOfficer = vm.OfficerIds.Select(id => new TrafficOfficer
    {
        OfficerId = id,
        TrafficPlaneId = entity.Id
    }).ToList();

    await _unitOfWork.TrafficPlane.SaveAsync(entity, name);
    await _unitOfWork.CompleteAsync();
}
   
    public async Task<List<TrafficPlanVm>> GetAllTraffic(DateOnly? date)
{
    var query = _unitOfWork.TrafficPlane.GetQueryable(
        include: q => q
            .Include(t => t.TrafficPlaces)
                .ThenInclude(tp => tp.place)
            .Include(t => t.TrafficOfficer)
                .ThenInclude(to => to.officer)
                .ThenInclude(r => r.Rank)
    );

    if (date.HasValue)
        query = query.Where(x => x.dateOnly == date.Value);

    var data = await query.ToListAsync();

    return data.Select(t => new TrafficPlanVm
    {
        id = t.Id,
        dateOnly = t.dateOnly,
        PlaceName = t.TrafficPlaces.FirstOrDefault()?.place?.Name,
        OfficerName = t.TrafficOfficer.FirstOrDefault()?.officer?.FullName,
        RankName = t.TrafficOfficer.FirstOrDefault()?.officer?.Rank?.Name
    }).ToList();
}

    



 private async Task<List<SelectListItem>> GetOfficers(
       
        )
    {
        return (await _unitOfWork.Officers.GetAllAsync(
                x => 
                     x.IsActive))
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FullName
            })
            .ToList();
    }


     private async Task<List<SelectListItem>> GetRank(
       
        )
    {
          return (await _unitOfWork.Ranks.GetAllAsync(x => x.IsActive))
        .Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.Name
        })
        .ToList();
    }



     private async Task<List<SelectListItem>> GetPlace(
       
        )
    {
        return (await _unitOfWork.Places.GetAllAsync(
                x => 
                     !x.IsDeleted))
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            })
            .ToList();
    }
    


    public async Task<TrafficPlanCreateVm> GetCreateData()
{
    return new TrafficPlanCreateVm
    {
        Officers = await GetOfficers(),
        Places = await GetPlace(),
    };
}
}



