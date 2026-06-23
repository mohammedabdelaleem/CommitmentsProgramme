
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommitmentsProgramme.Mvc.ViewModels;

public class CreateCommitmentVm
{
  [Display(Name = "التاريخ")]
  public DateOnly CommitmentDate { get; set; }

  [Display(Name = "الضابط العظيم")]
  public int SeniorOfficerId { get; set; }

  [Display(Name = "الضابط النوبتجي")]
  public int DutyOfficerId { get; set; }

  [Display(Name = "نوع الالتزام")]
  public int CommitmentTypeId { get; set; }

  [Display(Name = "الأولوية")]
  public int PriorityId { get; set; }

  [Display(Name = "الفروع المختصة")]
  public List<int> BranchIds { get; set; } = new();

  [Display(Name = "اسم الالتزام")]
  public string Title { get; set; } = string.Empty;

  [Display(Name = "التوقيت")]
  public TimeOnly? Time { get; set; }

  [Display(Name = "المكان")]
  public string? Location { get; set; }

  [Display(Name = "الحضور")]
  public string? Attendance { get; set; }

  [Display(Name = "ملاحظات")]
  public string? Notes { get; set; }

  public IEnumerable<SelectListItem> CommitmentTypes { get; set; } = [];

  public IEnumerable<SelectListItem> Priorities { get; set; } = [];

  public IEnumerable<SelectListItem> Branches { get; set; } = [];

  public IEnumerable<SelectListItem> SeniorOfficers { get; set; } = [];

  public IEnumerable<SelectListItem> DutyOfficers { get; set; } = [];
}