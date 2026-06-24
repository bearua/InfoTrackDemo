using Domain;
using GrabberAbstractions.Interfaces;
using InfoTrackDemo.DTO;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace InfoTrackDemo.Services;

public class LocationsService(
    AppDataContext dataContext,
    IContactGrabber grabber)
{
    public async Task<LocationDto[]> GetLocations()
    {
        var locations = await dataContext.Database.SqlQuery<LocationDto>(
            $"""
             select l."Id", l."Title", l."Text", l."LastUpdated",
                    count(c.*) as CountAll,
                    sum(case when c."IsNew" = true then 1  else 0 end) as CountNew
             from public."Locations" l
             left join public ."Contacts" c on l."Title" = c."Location"
             group by l."Id", l."Title", l."Text", l."LastUpdated"
             """).ToArrayAsync();
        
        //var locations = await dataContext.Locations.ToArrayAsync();
        return locations;
    }

    public async Task<Location?> AddLocation(string title, string text)
    {
        var item = new Location()
        {
            Title = title,
            Text = text
        };
        dataContext.Locations.Add(item);
        var result = await dataContext.SaveChangesAsync();
        if (result > 0)
        {
            return item;
        }

        return null;
    }

    public async Task<int> DeleteLocation(string title)
    {
        var item = dataContext.Locations.FirstOrDefault(l => l.Title == title);
        if (item != null)
        {
            dataContext.Locations.Remove(item);
            var result = await dataContext.SaveChangesAsync();
            return result;
        }

        return 0;
    }


    public async Task<Location[]> SearchLocations(string query)
    {
        var result = await grabber.GetLocationsSearch(query);
        return result;
    }
}