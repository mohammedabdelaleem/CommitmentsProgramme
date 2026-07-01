using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using CommitmentsProgramme.Utilities.Abstractions.Consts;
using static CommitmentsProgramme.Utilities.Abstractions.Consts.SharedData;

namespace CommitmentsProgramme.Utilities.Extensions;

public static class EnumExtensions
{
 public static string GetDisplayName(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());

        var attribute = field?.GetCustomAttributes(typeof(DisplayAttribute), false)
                              .Cast<DisplayAttribute>()
                              .FirstOrDefault();

        return attribute?.Name ?? value.ToString();
    }

	
}
