using CommitmentsProgramme.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
namespace CommitmentsProgramme.Mvc.Controllers
{
    public class TrafficPlanController(
        ITrafficServices trafficServices,
        IUnitOfWork unitOfWork) : Controller
    {
        private readonly ITrafficServices _trafficServices = trafficServices;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<IActionResult> Index(DateOnly? date)
        {
            var data = await _trafficServices.GetAllTraffic(date);


            return View(data);
        }
        //============                    
        // ADD
        //==========================
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]TrafficPlanAddVm vm)
        {
            var userId = User.GetUserId();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _trafficServices.AddTraffic(vm,userId) ;
            return Ok();
        }

        //==========================
        // EDIT 
        //==========================
        [HttpPost]
        public async Task<IActionResult> Edit([FromBody]TrafficPlanEditVm vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = User.GetUserId();
            await _trafficServices.EditTraffic(vm, userId);


            return Ok();
        }

        //==========================
        // DELETE
        //==========================
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _trafficServices.DeleteTraffic(id);


            return Ok();
        }



        [HttpGet]
public async Task<IActionResult> GetCreateData()
{
    var data = await _trafficServices.GetCreateData();
    return Json(data);
}
    }
}  







