using CommitmentsProgramme.Domain.Entities;

namespace CommitmentsProgramme.Utilities.Abstractions.Consts;

public static class SharedData
{
    public enum SettingsPhotoes
    {
        Logo = 1,
        MiddleBanner = 2,
        LastBanner = 3
    }

    public const string MainTitle = "Dashboard_X";

    public const int MaxNumberOfReports = 5;

    public enum OrderBy
    {
        Ascending,
        Descending
    }

  
    public enum SidebarOptions
    {
        Feed,
        Favourites,
        Friends,
        Settings
    }

 

    public static class CustomeClaims
    {
        public static string FullName = "FullName";
    }

}
