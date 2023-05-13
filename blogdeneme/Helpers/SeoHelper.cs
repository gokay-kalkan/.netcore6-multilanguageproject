using System.Text.RegularExpressions;

namespace blogdeneme.Helpers
{
    public static class SeoHelper
    {
        public static string GenerateSlug(string phrase)
        {
            string str = phrase.ToLower();

            // Remove invalid characters
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");

            // Convert spaces to hyphens
            str = Regex.Replace(str, @"\s+", "-").Trim();

            // Trim it to max 75 chars
            str = str.Substring(0, str.Length <= 75 ? str.Length : 75).Trim();

            // Replace double occurences of - or \_
            str = Regex.Replace(str, @"([-_]){2,}", "$1");

            // Trim any remaining hyphens or underscores from the end
            str = str.TrimEnd(new char[] { '-', '_' });

            return str;
        }

    }
}
