using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using ResumeBuilder.Domain;
using ResumeBuilder.Domain.Abstractions.Application;

namespace ResumeBuilder.Application.Abstractions;

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
