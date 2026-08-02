using api.Models;
using api.Utils;
using SnowflakeGenerator;

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
    Snowflake snowflake = new Snowflake();
    url.Id = (ulong)snowflake.NextID();
    url.ShortCode = Base62.Encode(url.Id);

    app.Logger.LogInformation($"[{DateTime.UtcNow}] Created short code \"{url.ShortCode}\" for target URL \"{url.OriginalUrl}\" (Status: 201 Created)");

    return Results.StatusCode(201);
});

app.Run();