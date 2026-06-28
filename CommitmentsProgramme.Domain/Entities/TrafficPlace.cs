namespace CommitmentsProgramme.Domain.Entities;

public class TrafficPlace : BaseEntity
{
  public int PlaceId {get;set;}
  public int TrafficId {get;set;}
  public Place place {get;set;}
  public TrafficPlane trafficPlane {get;set;}
}
