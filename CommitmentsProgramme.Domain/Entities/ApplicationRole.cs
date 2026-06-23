using Microsoft.AspNetCore.Identity;

namespace CommitmentsProgramme.Domain.Entities;
public class ApplicationRole : IdentityRole
{
	public ApplicationRole()
	{

		Id = Guid.CreateVersion7().ToString();
		ConcurrencyStamp = Guid.CreateVersion7().ToString();
	}

  [Required(ErrorMessage = ValidationMessages.Required)]
  [MaxLength(150)]
  public string DisplayName { get; set; } = string.Empty;

	public bool IsDefault { get; set; }
	public bool IsDeleted { get; set; }
	public DateTime CreatedAt { get; set; }
}


