using api.Interfaces;
using api.Services;
using api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IUrlService, UrlService>();

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

apiGroup.MapPost("/urls", async (IUrlService service, Url url) =>
{
    Url shortenedUrl  = await service.CreateShortenedUrl(url);
    return Results.Json(new { shortCode = shortenedUrl .ShortCode}, statusCode: 201);
});

app.Run();