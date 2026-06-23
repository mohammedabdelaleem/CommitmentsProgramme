namespace CommitmentsProgramme.Mvc.Mapping;

public class MappingConfigurations : IRegister
{
  public void Register(TypeAdapterConfig config)
  {


    config.NewConfig<UpdateProfileVM, ApplicationUser>()
    .Ignore(nameof(ApplicationUser.Email));



  }
}

