namespace CommitmentsProgramme.Utilities.Abstractions.RegEx;

public static class RegexPatterns
{
	public const string Password = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$";
  public const string Phone = @"^01[0125][0-9]{8}$";
}


