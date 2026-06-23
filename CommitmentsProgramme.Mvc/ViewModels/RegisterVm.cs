namespace CommitmentsProgramme.Mvc.ViewModels;
public class RegisterVm
{
	public RegisterRequestVm RegisterReq { get; set; } = new RegisterRequestVm();
	public List<ApplicationRole> Roles { get; set; } = [];
}


