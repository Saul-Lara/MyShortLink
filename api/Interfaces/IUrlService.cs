using api.Models;

namespace api.Interfaces
{
    public interface IUrlService
    {
        Task<Url> CreateShortenedUrl(Url urlData);
    }
}