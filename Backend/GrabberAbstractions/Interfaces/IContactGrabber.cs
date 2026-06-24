using Domain;

namespace GrabberAbstractions.Interfaces;

public interface IContactGrabber
{
    public Task<Contact[]> GetContactsByLocation(string location);
    
    public Task<Location[]> GetLocationsSearch(string query);
}