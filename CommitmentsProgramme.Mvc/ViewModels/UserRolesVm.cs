using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommitmentsProgramme.Mvc.ViewModels;

public class UserRolesVm
{
	public ApplicationUser User { get; set; }
	public List<string> UserRoles { get; set; } = [];
}


