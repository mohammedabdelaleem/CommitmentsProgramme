
using Microsoft.AspNetCore.Identity;

namespace CommitmentsProgramme.Domain.Entities;

public class ApplicationUser : IdentityUser
{
	public ApplicationUser()
	{
		Id = Guid.CreateVersion7().ToString();
		ConcurrencyStamp = Guid.CreateVersion7().ToString();
	}

  [Required(ErrorMessage = ValidationMessages.Required)]
  [MaxLength(300)]
  [Display(Name = "الاسم")]
  public string FullName { get; set; } = string.Empty;

  [Display(Name = "الحساب معطل")]
  public bool IsDisabled { get; set; }

  public DateTime CreatedAt { get; set; }

  [Display(Name = "السيرة الذاتية")]
  public string? Bio { get; set; }

}


