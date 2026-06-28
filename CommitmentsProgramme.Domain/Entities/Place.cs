namespace CommitmentsProgramme.Domain.Entities
{
    public class Place : BaseEntity
    {
        [Required(ErrorMessage = "{0} مطلوب")]
        [Display(Name = "اسم المكان")]
        [StringLength(300)]
        public string Name { get; set; }
          public bool IsDeleted { get; set; }

    public ICollection<TrafficPlace> TrafficPlaces { get; set; }
        = new List<TrafficPlace>();
    }
}
