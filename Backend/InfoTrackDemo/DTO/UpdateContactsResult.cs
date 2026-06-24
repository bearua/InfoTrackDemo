using Domain;

namespace InfoTrackDemo.DTO;

public class UpdateContactsResult
{
    public bool Success { get; set; }
    
    public string? Message { get; set; }
    
    public Location? Target { get; set; }
    
    public int OldCount { get; set; }
    
    public int NewCount { get; set; }
    
    public int GrabbedCount { get; set; }
}