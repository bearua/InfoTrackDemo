using InfoTrackDemo.Services;

namespace InfoTrackDemo.Endpoints;

public static class LocationEndpoints
{
    public static IEndpointRouteBuilder MapLocationEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/locations").WithTags("Locations");

        group.MapGet("/", GetLocations);
        group.MapPost("/", AddLocation);
        group.MapDelete("/", DeleteLocation);
        group.MapGet("/search", SearchLocations);

        return app;
    }

    private static async Task<IResult> GetLocations(
        LocationsService service)
    {
        var locations = await service.GetLocations();
        return Results.Json(locations);
    }

    private static async Task<IResult> AddLocation(
        LocationsService service,
        string title,
        string text)
    {
        var item = await service.AddLocation(title, text);
        return Results.Json(item);
    }

    private static async Task<IResult> DeleteLocation(
        LocationsService service,
        string title)
    {
        var result = await service.DeleteLocation(title);
        if (result == 0)
        {
            return Results.NotFound(title);
        }
        return Results.Ok(title);
    }
    
    private static async Task<IResult> SearchLocations(
        LocationsService service,
        string query)
    {
        var result = await service.SearchLocations(query);
        return Results.Json(result);
    }
}