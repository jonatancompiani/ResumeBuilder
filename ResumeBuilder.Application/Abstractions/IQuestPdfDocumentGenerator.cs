using QuestPDF.Infrastructure;
using ResumeBuilder.Domain;

namespace ResumeBuilder.Application.Abstractions;

public interface IQuestPdfDocumentGenerator
{
    IDocument GenerateDocument(ResumeData resumeData);
}
