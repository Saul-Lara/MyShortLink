using api.Models;
using api.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    var response = new
    {
        message = "Hello URL Shortener",
    };

    return response;
});

// API Prefix Group
var apiGroup = app.MapGroup("/api");

apiGroup.MapPost("/urls", (Url url) =>
{
    url.Id = 2026;
    string shortCode = Base62.Encode(url.Id);

    Console.WriteLine("Base62 Encoding");
    Console.WriteLine($"URL Id: {url.Id} - Encoded: {shortCode}");
    app.Logger.LogInformation($"[{DateTime.UtcNow}] Created short code \"{shortCode}\" for target URL \"{url.OriginalUrl}\" (Status: 201 Created)");

    return Results.StatusCode(201);
});

app.Run();