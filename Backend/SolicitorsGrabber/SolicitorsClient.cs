using System.Net;
using GrabberAbstractions.Interfaces;

namespace SolicitorsGrabber;

public class SolicitorsClient: IGrabClient
{
    private HttpClient _client = new();
    private const string BaseUrl = "https://www.solicitors.com/";
    private const string LocationsUrl = $"{BaseUrl}scripts/locations.asp?ajax=1&q=";
    private const string ContactsSuffix = "-solicitors.html";

    public async Task<string> GetRawDataByLocation(string location)
    {
        var url = $"{BaseUrl}{location}{ContactsSuffix}";
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Add("User-Agent","PostmanRuntime/7.32.3");
        var response = await _client.SendAsync(request);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new Exception("Failed to get contacts.");
        }
        var result = await response.Content.ReadAsStringAsync();
        return result;
    }

    public async Task<string> GetLocationsSearch(string query)
    {
        var url = LocationsUrl + query;
        var response = await _client.GetAsync(url);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new Exception("Failed to get locations.");
        }
        var result = await response.Content.ReadAsStringAsync();
        return result;
    }
}