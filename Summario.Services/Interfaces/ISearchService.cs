
namespace Summario.Services.Interfaces
{
    public interface ISearchService
    {
        Task<string> SearchAsync(string query);
    }
}
