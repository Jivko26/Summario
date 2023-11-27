

namespace Summario.Services.Interfaces
{
    public interface ITranslationService
    {
        Task<string> Translate(string summary);
    }
}
