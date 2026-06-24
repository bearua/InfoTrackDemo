using Domain;

namespace SolicitorsGrabber;

public interface ISingleContactParser
{
    bool IsMatch(string rawData);
    
    Contact ParseSingleContact(string rawData);
}