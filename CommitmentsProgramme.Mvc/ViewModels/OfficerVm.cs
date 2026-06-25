using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommitmentsProgramme.Mvc.ViewModels
{
    public class OfficerVm
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "اسم الضابط")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "الرتبة")]
        public int RankId { get; set; }

        public bool IsActive { get; set; } = true;

        public IEnumerable<SelectListItem> Ranks { get; set; }
            = new List<SelectListItem>();

    }
}
