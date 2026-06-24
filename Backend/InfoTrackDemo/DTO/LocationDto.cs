namespace InfoTrackDemo.DTO;

public class LocationDto
{
    public int Id { get; set; }
    
    public required string Title { get; set; }

    public required string Text { get; set; }
    
    public DateTime? LastUpdated { get; set; } 

    public int CountAll { get; set; }
    
    public int CountNew { get; set; }
}