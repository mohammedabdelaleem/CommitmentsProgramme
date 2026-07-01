namespace CommitmentsProgramme.Domain.Entities
{
    public class Shifts : BaseEntity
    {
        [Required(ErrorMessage = "{0} مطلوب")]
        [Display(Name = "اسم ")]
        [StringLength(300)]
        public string Name { get; set; }
          public ShiftRank shiftRank {get;set;} 
        public string Phone {get;set;}

        public DateOnly dateOnly {get;set;}
    }




   public  enum  ShiftRank{
            [Display(Name = "ظابط عظيم")]

     GreateOfficer,
         [Display(Name = "ضابط نوبنجي الاداره العامه")]

     ShiftOfficerGeneral,
              [Display(Name = "ضابط نوبنجي الاداره المحليه")]

     ShiftofficerLocal
        
    }
}
