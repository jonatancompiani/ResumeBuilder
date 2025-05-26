using QuestPDF.Infrastructure;
using ResumeBuilder.Domain;
using System.Collections.Generic;

namespace ResumeBuilder.Application
{
    public class ImageGenerator : IImageGenerator
    {
        private readonly IQuestPdfDocumentGenerator _documentGenerator;

        public ImageGenerator(IQuestPdfDocumentGenerator documentGenerator)
        {
            _documentGenerator = documentGenerator;
        }

        public IEnumerable<byte[]> GenerateImages(ResumeData resumeData)
        {
            IDocument document = _documentGenerator.GenerateDocument(resumeData);
            return document.GenerateImages();
        }
    }
}
