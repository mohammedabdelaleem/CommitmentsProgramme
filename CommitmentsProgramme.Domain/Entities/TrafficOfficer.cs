using System.Collections;

namespace CommitmentsProgramme.Domain.Entities;
public class TrafficOfficer :BaseEntity
{
     public int TrafficPlaneId {get;set;}
     public int OfficerId {get;set;}
     public Officer officer {get;set;}
     public TrafficPlane  trafficPlane {get;set;}
 

}