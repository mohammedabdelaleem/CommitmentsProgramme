using System.ComponentModel.DataAnnotations;

namespace CommitmentsProgramme.Mvc.ViewModels;

public class TrafficPlanEditVm
{

    public int Id { get; set; }
    public DateOnly DateOnly { get; set; }

    public List<int> PlaceIds { get; set; }
    public List<int> OfficerIds { get; set; }

}