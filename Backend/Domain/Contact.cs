namespace Domain;

public class Contact
{
    public int Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string Location { get; set; }
    
    public string? Address { get; set; }
    
    public string? Phone { get; set; }
    
    public int? StarsCount { get; set; }

    public int? VotesCount { get; set; }
    
    public bool? IsNew  { get; set; }
}