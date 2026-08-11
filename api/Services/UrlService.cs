using api.Interfaces;
using api.Models;
using api.Utils;
using SnowflakeGenerator;

namespace api.Services
{
    public class UrlService : IUrlService
    {
        private readonly Dictionary<string, string> _urlDatabase = new();
        private readonly ILogger<UrlService> _logger;

        public UrlService(ILogger<UrlService> logger)
        {
            _logger = logger;
        }

        public Task<Url> CreateShortenedUrl(Url urlData)
        {
            Snowflake snowflake = new Snowflake();
            urlData.Id = (ulong)snowflake.NextID();
            urlData.ShortCode = Base62.Encode(urlData.Id);

            _urlDatabase.Add(urlData.ShortCode, urlData.OriginalUrl);

            _logger.LogInformation(
            "Created short code {ShortCode} for target URL {OriginalUrl}",
            urlData.ShortCode,
            urlData.OriginalUrl
        );
            
            return Task.FromResult(urlData);
        }

        public Task<string?> GetOriginalUrl(string shortCode)
        {
            _urlDatabase.TryGetValue(shortCode, out string? originalURL);
            return Task.FromResult(originalURL);
        }
    }
}