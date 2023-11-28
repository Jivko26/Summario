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
            var dataFolderPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), $"Data/{file.FileName}");

            // Create the Data folder if it doesn't exist
            if (!Directory.Exists(dataFolderPath))
            {
                Directory.CreateDirectory(dataFolderPath);
            }

            var filePath = System.IO.Path.Combine(dataFolderPath, file.FileName);

            // Save the file to the Data folder
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

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

        public async Task<string> FindFileInDataFolder(string fileName)
        {
            // Determine the path for the Data folder
            var dataFolderPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Data");

            // Check if the Data folder exists
            if (!Directory.Exists(dataFolderPath))
            {
                return "Data folder does not exist.";
            }

            // Combine the file name with the Data folder path
            var filePath = System.IO.Path.Combine(dataFolderPath, fileName);

            // Check if the file exists
            if (File.Exists(filePath))
            {
                return filePath; // Or return any other relevant information
            }
            else
            {
                return "File not found.";
            }
        }

    }
}
