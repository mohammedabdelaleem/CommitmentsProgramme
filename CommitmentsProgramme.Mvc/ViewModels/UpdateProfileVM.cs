using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommitmentsProgramme.Mvc.ViewModels;

public class UpdateProfileVM
{
  [Required(ErrorMessage = "{0} مطلوب")]
  [MaxLength(300)]
  [Display(Name = "الاسم")]
  public string FullName { get; set; }

  [Required(ErrorMessage = "{0} مطلوب")]
  [StringLength(225, MinimumLength = 8)]
  [Display(Name = "اسم المستخدم")]
  public string UserName { get; set; }

  [ValidateNever]
  [Display(Name = "البريد الإلكتروني")]
  public string Email { get; set; }

    [Display(Name = "السيرة الذاتية")]
    public string Bio { get; set; }

}