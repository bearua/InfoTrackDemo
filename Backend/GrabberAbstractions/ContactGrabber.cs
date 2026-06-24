using Domain;
using GrabberAbstractions.Interfaces;

namespace GrabberAbstractions;

public class ContactGrabber(
    IGrabClient grabClient,
    IContactParser parser) : IContactGrabber
{
    public async Task<Contact[]> GetContactsByLocation(string location)
    {
        var rawData = await grabClient.GetRawDataByLocation(location);
        var result = await parser.ParseContacts(rawData);
        foreach (var contact in result)
        {
            contact.Location = location;
        }
        return result;
    }

    public async Task<Location[]> GetLocationsSearch(string query)
    {
        var rawData = await grabClient.GetLocationsSearch(query);
        var result = await parser.ParseLocations(rawData);
        return result;
    }
}