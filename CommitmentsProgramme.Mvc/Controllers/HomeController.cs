using Microsoft.AspNetCore.Mvc;

namespace CommitmentsProgramme.Mvc.Controllers
{
  public class HomeController(IUnitOfWork unitOfWork, IConfiguration configuration) : Controller
  {
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IConfiguration _configuration = configuration;

  
    public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
    {
      return View();
    }

   
    public IActionResult NotFound() => View();

  }
}


