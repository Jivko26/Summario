
namespace Summario.Services.Interfaces
{
    public interface IGptSummarizerService
    {
        Task<List<string>> SummarizeAsync(string text);
    }

}
