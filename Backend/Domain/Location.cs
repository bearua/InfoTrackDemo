namespace Domain;

public class Location
{
    public int Id { get; set; }
    
    public required string Title { get; set; }

    public required string Text { get; set; }
    
    public DateTime? LastUpdated { get; set; } 
}