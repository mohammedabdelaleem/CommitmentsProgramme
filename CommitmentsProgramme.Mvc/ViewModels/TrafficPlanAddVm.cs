using System.ComponentModel.DataAnnotations;

namespace CommitmentsProgramme.Mvc.ViewModels;

public class TrafficPlanAddVm
{

    public DateOnly DateOnly { get; set; }

    public List<int> PlaceIds { get; set; }
    public List<int> OfficerIds { get; set; }

}