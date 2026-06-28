using System.Collections;

namespace CommitmentsProgramme.Domain.Entities;
public class TrafficPlane :BaseEntity
{
     public DateOnly dateOnly {get;set;}

         public ICollection<TrafficPlace> TrafficPlaces { get; set; }
        = new List<TrafficPlace>();


         public ICollection<TrafficOfficer> TrafficOfficer { get; set; }
        = new List<TrafficOfficer>();

}


