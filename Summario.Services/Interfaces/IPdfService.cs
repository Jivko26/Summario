

using Microsoft.AspNetCore.Http;

namespace Summario.Services.Interfaces
{
    public interface IPdfService
    {
        Task<string> ExtractTextFromPdfAsync(IFormFile file);
        Task<string> FindFileInDataFolder(string fileName);
    }
}
