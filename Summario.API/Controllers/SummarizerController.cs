
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

    [HttpPost]
    public async Task<IActionResult> UploadPdf(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Upload a file.");

        string text = await this._pdfService.ExtractTextFromPdfAsync(file);
        var summaries = await this._summarizerService.SummarizeAsync(text);

        return Ok(summaries);
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
