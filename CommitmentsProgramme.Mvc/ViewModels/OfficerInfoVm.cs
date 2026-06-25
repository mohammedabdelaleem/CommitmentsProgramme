namespace CommitmentsProgramme.Mvc.ViewModels
{
    public class OfficerInfoVm
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "اسم الضابط")]
        public string FullName { get; set; } 

        [Display(Name = "الرتبة")]
        public string RankName { get; set; }

        [Display(Name = "المحمول")]
        public string PhoneNumber { get; set; }
    }
}
