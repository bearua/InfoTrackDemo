namespace GrabberAbstractions.Interfaces;

public interface IGrabClient
{
    Task<string> GetRawDataByLocation(string location);

    Task<string> GetLocationsSearch(string query);
}