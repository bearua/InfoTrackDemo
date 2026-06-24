using Domain;

namespace GrabberAbstractions.Interfaces;

public interface IContactParser
{
    Task<Contact[]> ParseContacts(string rawData);
    
    Task<Location[]> ParseLocations(string rawData);
}