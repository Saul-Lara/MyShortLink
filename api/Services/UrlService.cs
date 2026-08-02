using api.Interfaces;
using api.Models;
using api.Utils;
using SnowflakeGenerator;

namespace api.Services
{
    public class UrlService : IUrlService
    {
        public Task<Url> CreateShortenedUrl(Url urlData)
        {
            Snowflake snowflake = new Snowflake();
            urlData.Id = (ulong)snowflake.NextID();
            urlData.ShortCode = Base62.Encode(urlData.Id);
            
            return Task.FromResult(urlData);
        }
    }
}