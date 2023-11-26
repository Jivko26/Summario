
using Microsoft.AspNetCore.Mvc;
using Summario.Services.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class SummarizerController : Controller
{
    private readonly IGptSummarizerService _summarizerService;
    private readonly IPdfService _pdfService;

    public SummarizerController(IGptSummarizerService summarizerService, IPdfService pdfService)
    {
        _summarizerService = summarizerService;
        _pdfService = pdfService;
    }

    [HttpPost("SummarizeArtilce")]
    public async Task<IActionResult> SummarizeArtilce(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
                return BadRequest("Upload a file.");

            string text = await this._pdfService.ExtractTextFromPdfAsync(file);
            var summaries = await this._summarizerService.SummarizeAsync(text);

            return Ok(summaries);
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            throw;
        }
        
    }

    [HttpGet("SearchFile")]
    public async Task<IActionResult> SearchFile(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return BadRequest("Upload a file.");

        string result = await this._pdfService.FindFileInDataFolder(fileName);

        return Ok(result);
    }
}
