using System.ComponentModel.DataAnnotations;

namespace CommitmentsProgramme.Mvc.ViewModels;

public class TrafficPlanVm
{

public int id {get ;set ;}
    public string PlaceName { get; set; }
    public string RankName { get; set; } 
    public string OfficerName { get; set; } 
    public DateOnly dateOnly {get;set;}
}