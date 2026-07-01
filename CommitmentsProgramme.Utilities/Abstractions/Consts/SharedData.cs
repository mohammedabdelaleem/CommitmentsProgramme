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


    public enum OrderBy
    {
        Ascending,
        Descending
    }

    public static class CustomeClaims
    {
        public static string FullName = "FullName";
        public static string ImageName = "ImageName";
    }

    public class Messages
    {
        public const string SuccessRemoveItem = "تم حذف اعنصر بنجاح";
        public const string ErrorRemoveItem = "لا يمكن تنفيذ العملية لأن هذا العنصر مرتبط ببيانات أخرى في النظام. يرجى إزالة الارتباطات أولاً ثم إعادة المحاولة. ";
        public const string ItemNotFound = "لا يمكن تنفيذ العملية لأن هذا العنصر مرتبط ببيانات أخرى في النظام. يرجى إزالة الارتباطات أولاً ثم إعادة المحاولة. ";

    }


}