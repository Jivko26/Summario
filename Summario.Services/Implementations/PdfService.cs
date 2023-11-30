using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.AspNetCore.Http;
using Summario.Services.Interfaces;
using System.Text;

namespace Summario.Services.Implementations
{
    public class PdfService : IPdfService
    {
        public async Task<string> ExtractTextFromPdfAsync(IFormFile file)
        {

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            using var reader = new PdfReader(memoryStream);
            var text = new StringBuilder();

            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
                text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
            }

            return text.ToString();
        }

    }
}
