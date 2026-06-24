using InfoTrackDemo.Services;

namespace InfoTrackDemo.Endpoints;

public static class ContactEndpoints
{
    public static IEndpointRouteBuilder MapContactEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/contacts").WithTags("Contacts");

        group.MapGet("/", GetContacts);
        group.MapPost("/update", UpdateContacts);

        return app;
    }

    private static async Task<IResult> UpdateContacts(
        ContactsService contactsService, 
        string location)
    {
        var result = await contactsService.UpdateContacts(location);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetContacts(
        ContactsService contactsService)
    {
        var result = await contactsService.GetContacts();
        return Results.Json(result);
    }
}