using APMLibrary.Bll.Services.Interfaces;
using iTextSharp.text.log;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Services.Implements
{
    public static class TextFileExtension : object
    {
        public static IServiceCollection AddTextFileService(this IServiceCollection services)
        {
            return services.AddTransient<ITextFileReader, TextFileReader>();
        }
    }
    public partial class TextFileReader : ITextFileReader
    {
        private readonly ILogger<TextFileReader> logger = default!;
        public TextFileReader(ILogger<TextFileReader> logger) : base() => this.logger = logger;

        public virtual async Task<string?> ReadPageAsync(byte[] fileData, int pageNumber)
        {
            await using var resultPage = new StringWriter();
            using (var pdfReader = new PdfReader(fileData))
            {
                if (pageNumber > pdfReader.NumberOfPages || pageNumber < 1) return null;
                var pageText = PdfTextExtractor.GetTextFromPage(pdfReader, pageNumber, new LocationTextExtractionStrategy());

                foreach (var line in pageText.Split('\n'))
                {
                    await resultPage.WriteLineAsync($"{line}");
                }
            }
            return resultPage.ToString();
        }
        public virtual int GetPagesCount(byte[] fileData) => new PdfReader(fileData).NumberOfPages;

        public virtual void Dispose() { }
    }
}
