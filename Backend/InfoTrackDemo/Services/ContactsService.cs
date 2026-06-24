using Domain;
using GrabberAbstractions.Interfaces;
using InfoTrackDemo.DTO;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace InfoTrackDemo.Services;

public class ContactsService(
    IContactGrabber grabber,
    AppDataContext dataContext)
{
    public async Task<Contact[]> GetContacts()
    {
        var result = await dataContext.Contacts.ToArrayAsync();
        return result;
    }

    public async Task<UpdateContactsResult> UpdateContacts(string location)
    {
        var result = new UpdateContactsResult()
        {
            Success = true,
            OldCount = 0,
            GrabbedCount = 0,
            NewCount = 0
        };
        
        var targetLocation = await dataContext.Locations.FirstOrDefaultAsync(l => l.Title == location);
        if (targetLocation == null)
        {
            return new UpdateContactsResult()
            {
                Success = false,
                Message = "Location Not Found"
            };
        }

        result.Target = targetLocation;
        
        var targetContacts = await dataContext.Contacts.Where(c => c.Location == location).ToDictionaryAsync(c => c.Name, c => c);
        
        result.OldCount = targetContacts.Count;
        var processedContacts = new Dictionary<string, Contact>();

        try
        {
            var grabData = await grabber.GetContactsByLocation(Normalize(location));
            result.GrabbedCount = grabData.Length;
            foreach (var contact in grabData)
            {
                if (targetContacts.TryGetValue(contact.Name, out var targetContact))
                {
                    if (processedContacts.TryAdd(contact.Name, contact))
                    {
                        UpdateSingleContact(contact, targetContact);
                    }
                }
                else
                {
                    if (processedContacts.TryAdd(contact.Name, contact))
                    {
                        AddSingleContact(contact, targetLocation);
                        result.NewCount++;
                    }
                }
            }
            targetLocation.LastUpdated  = DateTime.UtcNow;
            dataContext.Locations.Update(targetLocation);

            await dataContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new UpdateContactsResult()
            {
                Success = false,
                Message = $"Failed to grab contacts. Error:{e.Message}"
            };
        }
        return result;
    }

    private string Normalize(string location)
    {
        return location.Replace(" ", "-").ToLower();
    }

    private void AddSingleContact(Contact contact, Location targetLocation)
    {
        contact.IsNew = true;
        contact.Location = targetLocation.Title;
        dataContext.Contacts.Add(contact);
    }

    private void UpdateSingleContact(Contact contact, Contact targetContact)
    {
        targetContact.IsNew = false;
        targetContact.Phone = contact.Phone;
        targetContact.Address = contact.Address;
        targetContact.StarsCount = contact.StarsCount;
        targetContact.VotesCount = contact.VotesCount;
        dataContext.Contacts.Update(targetContact);
    }
}