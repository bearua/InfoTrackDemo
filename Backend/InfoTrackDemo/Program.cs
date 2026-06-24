using GrabberAbstractions;
using GrabberAbstractions.Interfaces;
using InfoTrackDemo.Endpoints;
using InfoTrackDemo.Services;
using Microsoft.EntityFrameworkCore;
using Persistence;
using SolicitorsGrabber;

var builder = WebApplication.CreateBuilder(args);

// Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDataContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IContactGrabber, ContactGrabber>();
builder.Services.AddScoped<IGrabClient, SolicitorsClient>();
builder.Services.AddScoped<IContactParser, SolicitorsParser>();
builder.Services.AddScoped<LocationsService, LocationsService>();
builder.Services.AddScoped<ContactsService, ContactsService>();

string myAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins(builder.Configuration["AllowedOrigins"] ?? "http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapLocationEndpoints();
app.MapContactEndpoints();
app.UseCors(myAllowSpecificOrigins);

// Apply migrations on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDataContext>();
    await db.Database.MigrateAsync();
}

app.Run();
