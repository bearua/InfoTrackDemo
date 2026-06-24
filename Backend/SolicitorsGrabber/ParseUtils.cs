using System.Text.RegularExpressions;

namespace SolicitorsGrabber;

public static class ParseUtils
{
    public static string ExtractData(string data, string pattern)
    {
        var result = Regex.Match(data, pattern,RegexOptions.Multiline);
        return result.Groups.Count > 1 ? result.Groups[1].Value : string.Empty;
    }

    public static int ExtractRating(string data)
    {
        var results = Regex.Matches(data, "star-(\\S*)\\s",RegexOptions.Multiline);
        return results.Sum(m =>
        {
            switch (m.Groups.Count > 1 ? m.Groups[1].Value : string.Empty)
            {
                case "full": return 2;
                case "half": return 1;
                default: return 0;
            }
        });
    }
}