using System.Text.Json;
using System.Text.RegularExpressions;
using Domain;
using GrabberAbstractions.Interfaces;
using SolicitorsGrabber.SingleContactParsers;

namespace SolicitorsGrabber;

public class SolicitorsParser: IContactParser
{
    private ISingleContactParser[]  _singleContactParsers = [new BigContactParser(), new SmallContactParser()];
    
    public async Task<Contact[]> ParseContacts(string rawData)
    {
        var nodes = new List<string>();
        var matches = Regex.Matches(rawData, "<div class=\"result-item");
        foreach (var startIndex in matches.Select(m => m.Index))
        {
            var endIndex = FindEndIdex(rawData, startIndex);
            nodes.Add(rawData.Substring(startIndex, endIndex - startIndex));
        }

        return nodes.Select(ParseSingleContact).ToArray();
    }

    private Contact ParseSingleContact(string data)
    {
        foreach (var parser in _singleContactParsers)
        {
            if (parser.IsMatch(data))
            {
                return parser.ParseSingleContact(data);
            }
        }
        throw new Exception("No parser found for contact.");
    }

    public async Task<Location[]> ParseLocations(string rawData)
    {
        var result = JsonSerializer.Deserialize<Location[]>(rawData, JsonSerializerOptions.Web);
        return result ?? [];
    }

    private int FindEndIdex(string rawData, int startIndex)
    {
        var sum = 0;
        var currentIndex = startIndex;
        
        while (sum != 0 || currentIndex == startIndex)
        {
            var openIndex = rawData.IndexOf("<div", currentIndex, StringComparison.Ordinal);
            var closeIndex = rawData.IndexOf("</div>", currentIndex, StringComparison.Ordinal);
            if (openIndex < closeIndex)
            {
                sum++;
                currentIndex = openIndex + 1;
            }
            else
            {
                sum--;
                currentIndex = closeIndex + 1;
            }
        }

        return currentIndex + 5;
    }
}