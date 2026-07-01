using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommitmentsProgramme.Utilities.Extensions
{
    public static class StringExtensions
    {
        public static string ToArabic(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            var en = CultureInfo.InvariantCulture;
            var ar = new CultureInfo("ar-EG");

            // Translate English day names
            if (DateTime.TryParseExact(value, "dddd", en, DateTimeStyles.None, out var day))
                return day.ToString("dddd", ar);

            // Convert digits
            return value
                .Replace('0', '٠')
                .Replace('1', '١')
                .Replace('2', '٢')
                .Replace('3', '٣')
                .Replace('4', '٤')
                .Replace('5', '٥')
                .Replace('6', '٦')
                .Replace('7', '٧')
                .Replace('8', '٨')
                .Replace('9', '٩');
        }
    }
}
