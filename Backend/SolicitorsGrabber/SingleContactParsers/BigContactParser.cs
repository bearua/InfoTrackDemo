using Domain;

namespace SolicitorsGrabber.SingleContactParsers;

public class BigContactParser : ISingleContactParser
{
    private const string Marker = "<div class=\"result-item\">"; 
    private const string Name = "<span class=\"h2\">([^<]*)<";
    private const string Address = "<address>(.*)</address>";
    private const string Phone = "<a rel=\"noindex\"[^>]*>([^<]*)<";
    private const string VoteCount = "<span class=\"rev-results\">[^(]*\\((\\d*)\\)</span>";

    public bool IsMatch(string data)
    {
        return data.StartsWith(Marker);
    }

    public Contact ParseSingleContact(string data)
    {
        var votesCount = ParseUtils.ExtractData(data, VoteCount);
        var result = new Contact
        {
            Id = 0,
            Name = ParseUtils.ExtractData(data, Name),
            Location = "",
            Address = ParseUtils.ExtractData(data, Address),
            Phone = ParseUtils.ExtractData(data, Phone),
            StarsCount = ParseUtils.ExtractRating(data),
            VotesCount = string.IsNullOrEmpty(votesCount) ? 0 : int.Parse(votesCount)
        };
        return  result;
    }
}