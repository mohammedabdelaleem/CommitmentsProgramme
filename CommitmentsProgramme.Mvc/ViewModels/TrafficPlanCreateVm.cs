using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommitmentsProgramme.Mvc.ViewModels;

public class TrafficPlanCreateVm
{

   public List<SelectListItem> Officers { get; set; }
    public List<SelectListItem> Places { get; set; }
    public List<SelectListItem> Ranks { get; set; }

}