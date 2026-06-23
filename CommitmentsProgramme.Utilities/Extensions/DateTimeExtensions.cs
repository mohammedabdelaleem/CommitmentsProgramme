using System.Security.Claims;
using System.Text.RegularExpressions;
using CommitmentsProgramme.Utilities.Abstractions.RegEx;

namespace CommitmentsProgramme.Utilities.Extensions;

public static class DateTimeExtensions
{
    public static string ToShortTimeAgo(this DateTime dateTime)
    {
        var timeSpan = DateTime.UtcNow - dateTime;

        if (timeSpan.TotalSeconds < 60)
            return "just now";

        if (timeSpan.TotalMinutes < 60)
            return $"{(int)timeSpan.TotalMinutes} min ago";

        if (timeSpan.TotalHours < 24)
            return $"{(int)timeSpan.TotalHours} h ago";

        if (timeSpan.TotalDays < 7)
            return $"{(int)timeSpan.TotalDays} d ago";

        if (timeSpan.TotalDays < 30)
            return $"{(int)(timeSpan.TotalDays / 7)} w ago";

        if (timeSpan.TotalDays < 365)
            return $"{(int)(timeSpan.TotalDays / 30)} mo ago";

        return $"{(int)(timeSpan.TotalDays / 365)} y ago";
    }
}
